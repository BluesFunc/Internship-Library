using Domain.Enums;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Books.Commands.UpdateBook;

public class UpdateBookCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Isbn { get; init; } = null!;
    public BookGenre Genre { get; init; }
    public string Description { get; init; } = null!;
    public string Image { get; init; } = null!;
    public Guid  AuthorId { get; init; }
}