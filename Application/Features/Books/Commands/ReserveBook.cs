using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Books.Commands;

public record ReserveBookCommand : IRequest<Result>
{
    public Guid BookId { get; init; }
    
}

public class ReserveBookHandler(IBookRepository repository,
    IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor) : IRequestHandler<ReserveBookCommand, Result>
{
    private int _bookingPeriod = 14;
    public async Task<Result> Handle(ReserveBookCommand request, CancellationToken cancellationToken)
    {
        var book = await repository.GetByIdAsync(request.BookId);
        if (book == null)
        {
            return Result.Failed("Not found a book");
        }
        var userId  = contextAccessor.HttpContext?.GetUserIdFromClaims();
        if (userId == null)
        {
            return Result.Failed("User id is missing");
        }

        book.BookedById = userId.Value;
        book.BookedAt = DateTime.UtcNow;
        book.BookingDeadline = DateTime.UtcNow.AddDays(_bookingPeriod);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Successful();
    }
}