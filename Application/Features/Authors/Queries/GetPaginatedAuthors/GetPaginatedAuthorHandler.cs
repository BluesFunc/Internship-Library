using Application.DTOs._Author_;
using Application.Interfaces.Repositories;
using Application.QueryParams;
using Application.Wrappers;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Authors.Queries.GetPaginatedAuthors;



public class GetPaginatedAuthorHandler(
    IAuthorRepository repository,
    IMapper mapper)
    : IRequestHandler<GetPaginatedAuthorsCommand, Result<PaginationList<AuthorDto>>>
{
    public async Task<Result<PaginationList<AuthorDto>>> Handle
        (GetPaginatedAuthorsCommand request, CancellationToken cancellationToken)
    {
        var queryParams = new AuthorQueryParams()
            { PageNo = request.PageNo, PageSize = request.PageSize };
        var paginationList = await repository.GetPaginatedCollectionAsync(queryParams);
        var data = mapper.Map<List<AuthorDto>>(paginationList);
        var content = new PaginationList<AuthorDto>()
        {
            Data = data,
            PageNo = queryParams.PageNo,
            PageSize = queryParams.PageSize
        };
        return Result<PaginationList<AuthorDto>>.Successful(content);
    }
}