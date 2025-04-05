using Application.Wrappers;
using MediatR;

namespace Application.Features.Books.Commands;

public record ReserveBookCommand : IRequest<Result>
{
    public Guid UserId { get; init; }
    public Guid BookId { get; init; }
    
}