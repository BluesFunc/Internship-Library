using Application.DTOs._Book_;
using Application.Interfaces.Repositories;
using Application.QueryParams;
using Application.Wrappers;
using MapsterMapper;
using MediatR;

namespace Application.Features.Books.Queries;

public class GetBooksByAuthorCommand : IRequest<Result<PaginationList<BookDto>>>
{
    public Guid AuthorId { get; init; }
    public int PageNo { get; init; } = 1;
    public int PageSize { get; init; } = 5;
}

public class GetBooksByAuthorHandler(
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    IMapper mapper)
    : IRequestHandler<GetBooksByAuthorCommand, Result<PaginationList<BookDto>>>
{
    public async Task<Result<PaginationList<BookDto>>> Handle(GetBooksByAuthorCommand request,
        CancellationToken cancellationToken)
    {
        var queryParams = new BookQueryParams()
        {
            AuthorId = request.AuthorId,
            PageSize = request.PageSize,
            PageNo = request.PageNo
        };
        var paginatedBooks = await bookRepository
            .GetPaginatedCollectionAsync(queryParams);
        var data = mapper.Map<List<BookDto>>(paginatedBooks);
        var paginatedContent = new PaginationList<BookDto>()
        {
            Data = data,
            PageSize = request.PageSize,
            PageNo = request.PageNo

        };
        return Result<PaginationList<BookDto>>.Successful(paginatedContent);
    }
}