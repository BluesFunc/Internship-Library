using Application.DTOs._Account_;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Auth.Commands.LoginUser;

public record LoginUserCommand : IRequest<Result<TokenPair>>
{
    public string Mail { get; init; }
    public string Password { get; init; }
}