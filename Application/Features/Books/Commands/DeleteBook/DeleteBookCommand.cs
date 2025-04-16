using Application.Interfaces.Requests;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Books.Commands.DeleteBook;

public record DeleteBookCommand : IRequest<Result>, ITransactionRequest
{
    public Guid Id { get; init; }
}

