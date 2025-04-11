using Application.Wrappers;
using MediatR;

namespace Application.Features.Authors.Commands.DeleteAuthorById;

public record DeleteAuthorByIdCommand : IRequest<Result>
{
    public Guid Id { get; init; }
}