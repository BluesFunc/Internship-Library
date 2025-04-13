using Application.DTOs._Book_;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Books.Queries.GetBookByIsbn;

public record GetBookByIsbnCommand : IRequest<Result<BookDto>>
{
    public string Isbn { get; init; } = null!;
}