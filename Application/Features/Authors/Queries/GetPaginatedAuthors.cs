using Application.DTOs._Author_;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MapsterMapper;
using MediatR;

namespace Application.Features.Authors.Queries;

public record GetPaginatedAuthorsCommand : IRequest<Result<PaginationList<AuthorDto>>>
{
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 5;
}

public class GetPaginatedAuthorHandler(
    IAuthorRepository repository,
    IMapper mapper)
    : IRequestHandler<GetPaginatedAuthorsCommand, Result<PaginationList<AuthorDto>>>
{
    public async Task<Result<PaginationList<AuthorDto>>> Handle
        (GetPaginatedAuthorsCommand request, CancellationToken cancellationToken)
    {
        var paginationList = await repository.GetPaginatedAsync(request.PageNo, request.PageSize);
        var data = mapper.Map<List<AuthorDto>>(paginationList.Data);
        var content = new PaginationList<AuthorDto>()
        {
            Data = data,
            PageNo = request.PageNo,
            PageSize = request.PageSize
        };
        return Result<PaginationList<AuthorDto>>.Successful(content);
    }
}