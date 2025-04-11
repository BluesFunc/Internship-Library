using Application.DTOs._Author_;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Authors.Queries.GetAuthorById;

public record GetAuthorByIdCommand : IRequest<Result<AuthorDto>>
{
    public Guid Id { get; set; }
}