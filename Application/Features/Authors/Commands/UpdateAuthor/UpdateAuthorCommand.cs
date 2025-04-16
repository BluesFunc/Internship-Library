using Application.Interfaces.Requests;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Authors.Commands.UpdateAuthor;

public record UpdateAuthorCommand : IRequest<Result>, ITransactionRequest
{
    public Guid Id { get; init; } 
    public string Name { get; init; } = null!;
    public string Surname { get; init; } = null!;
    public DateOnly BirthDate { get; init; }
    public string Country { get; init; } = null!;
}