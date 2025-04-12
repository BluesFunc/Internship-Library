using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Books.Commands.ReserveBook;

public record ReserveBookCommand : IRequest<Result>
{
    public Guid BookId { get; init; }
    
}