using Application.Interfaces.Requests;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Authors.Commands.DeleteAuthorById;

public record DeleteAuthorByIdCommand : IRequest<Result>, ITransactionRequest
{
    public Guid Id { get; init; }
}