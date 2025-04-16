using Application.DTOs._Author_;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Authors.Queries.GetPaginatedAuthors;

public record GetPaginatedAuthorsCommand : IRequest<Result<PaginationList<AuthorDto>>>
{
    public int PageNo { get; init; } = 1;
    public int PageSize { get; init; } = 5;
}