using Application.DTOs._Book_;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Books.Queries.GetPaginatedBooks;

public class GetPaginatedBooksCommand : IRequest<Result<PaginationList<BookDto>>>
{
    public int PageNo { get; init; } = 1;
    public int PageSize { get; init; } = 5;
}