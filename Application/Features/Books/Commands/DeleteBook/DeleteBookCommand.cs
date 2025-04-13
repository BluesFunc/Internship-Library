using Application.Interfaces.Requests;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Books.Commands.DeleteBook;

public record DeleteBookCommand : IRequest<Result>, ITransactionRequest
{
    public Guid Id { get; init; }
}

