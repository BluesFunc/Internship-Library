using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Books.Commands;

public record ReserveBookCommand : IRequest<Result>
{
    public Guid UserId { get; init; }
    public Guid BookId { get; init; }
    
}

public record ReserveBookHandler(IBookRepository Repository, IUnitOfWork UnitOfWork) : IRequestHandler<ReserveBookCommand, Result>
{
    public async Task<Result> Handle(ReserveBookCommand request, CancellationToken cancellationToken)
    {
        var book = await Repository.GetByIdAsync(request.BookId);
        if (book == null)
        {
            return Result.Failed("Not found a book");
        }

        book.BookedById = request.UserId;
        await UnitOfWork.SaveChangesAsync();
        return Result.Successful();
    }
}