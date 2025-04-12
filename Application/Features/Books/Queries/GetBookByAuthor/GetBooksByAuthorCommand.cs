using Application.DTOs._Book_;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Books.Queries.GetBookByAuthor;

public class GetBooksByAuthorCommand : IRequest<Result<PaginationList<BookDto>>>
{
    public Guid AuthorId { get; init; }
    public int PageNo { get; init; } = 1;
    public int PageSize { get; init; } = 5;
}