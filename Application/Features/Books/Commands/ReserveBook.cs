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
    private int BookingPeriod = 14;
    public async Task<Result> Handle(ReserveBookCommand request, CancellationToken cancellationToken)
    {
        var book = await Repository.GetByIdAsync(request.BookId);
        if (book == null)
        {
            return Result.Failed("Not found a book");
        }

        book.BookedById = request.UserId;
        book.BookedAt = DateTime.UtcNow;
        book.BookingDeadline = DateTime.UtcNow.AddDays(BookingPeriod);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Successful();
    }
}