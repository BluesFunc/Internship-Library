using Application.DTOs._Book_;
using Application.Interfaces.Requests;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Books.Commands.CreateBook;

public record CreateBookCommand : IRequest<Result<BookDto>>, ITransactionRequest
{
    public string Name { get; init; } = null!;
    public string Isbn { get; init; } = null!;
    public BookGenre Genre { get; init; }
    public string Description { get; init; } = null!;
    public string Image { get; init; } = null!;
    public Guid AuthorId { get; init; }
}