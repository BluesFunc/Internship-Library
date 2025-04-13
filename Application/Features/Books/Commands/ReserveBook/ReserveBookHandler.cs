using Application.Extensions;
using Domain.Interfaces.Repositories;
using Domain.Models.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Books.Commands.ReserveBook;

public class ReserveBookHandler(
    IBookRepository bookRepository,
    IUserRepository userRepository,
    IHttpContextAccessor contextAccessor) : IRequestHandler<ReserveBookCommand, Result>
{
    private int _bookingPeriod = 14;

    public async Task<Result> Handle(ReserveBookCommand request, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetByIdAsync(request.BookId, cancellationToken);
        if (book == null)
        {
            return Result.Failed("Book not found", ErrorTypeCode.NotFound);
        }

        if (book.IsReserved)
        {
            return Result.Failed("Book already reserved", ErrorTypeCode.EntityConflict);
        }

        var userId = contextAccessor.HttpContext?.GetUserIdFromClaims();
        if (userId == null)
        {
            return Result.Failed("User id is missing", ErrorTypeCode.NotAuthorized);
        }
        var user = await userRepository.GetByIdAsync(userId.Value, cancellationToken);
        if (user == null)
        {
            return Result.Failed("Given user does not exist", ErrorTypeCode.NotAuthorized);
        }
        
        var bookingDeadline = DateTime.UtcNow.AddDays(_bookingPeriod);
        book.ReserveBook(user, bookingDeadline );
        return Result.Successful();
    }
}