using Application.Wrappers;
using MediatR;

namespace Application.Features.Authors.Commands.CreateAuthor;

public record CreateAuthorCommand : IRequest<Result> 
{
    public string Name { get; init; } = null!;
    public string Surname { get; init; } = null!;
    public DateOnly BirthDate { get; init; }
    public string Country { get; init; } = null!;
}