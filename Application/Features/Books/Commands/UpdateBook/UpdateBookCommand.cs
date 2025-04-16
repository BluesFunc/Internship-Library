using Application.Interfaces.Requests;
using Application.Wrappers;
using Domain.Enums;
using MediatR;

namespace Application.Features.Books.Commands.UpdateBook;

public class UpdateBookCommand : IRequest<Result>, ITransactionRequest
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public BookGenre Genre { get; init; }
    public string Description { get; init; } = null!;
    public string Image { get; init; } = null!;
    public Guid  AuthorId { get; init; }
}