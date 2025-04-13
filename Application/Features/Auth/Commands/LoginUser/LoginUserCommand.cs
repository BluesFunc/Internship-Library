using Application.DTOs._Account_;
using Application.Interfaces.Requests;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Auth.Commands.LoginUser;

public record LoginUserCommand : IRequest<Result<TokenPair>>, ITransactionRequest
{
    public string Mail { get; init; }
    public string Password { get; init; }
}