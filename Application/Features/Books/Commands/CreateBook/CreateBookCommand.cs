using Application.DTOs._Book_;
using Application.Interfaces.Requests;
using Domain.Entities;
using Domain.Enums;
using Domain.Models.Wrappers;
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