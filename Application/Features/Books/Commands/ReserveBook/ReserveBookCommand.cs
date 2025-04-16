using Application.Interfaces.Requests;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Books.Commands.ReserveBook;

public record ReserveBookCommand : IRequest<Result>, ITransactionRequest
{
    public Guid BookId { get; init; }
    
}