using Application.DTOs._Book_;
using Domain.Interfaces.Repositories;
using Domain.Models.QueryParams;
using Domain.Models.Wrappers;
using MapsterMapper;
using MediatR;

namespace Application.Features.Books.Queries.GetBookByAuthor;

public class GetBooksByAuthorHandler(
    IBookRepository bookRepository,
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