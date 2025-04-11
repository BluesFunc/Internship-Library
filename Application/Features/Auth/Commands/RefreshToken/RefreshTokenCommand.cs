using Application.DTOs._Account_;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand : IRequest<Result<TokenPair>>
{
    public string RefreshToken { get; init; }
    
}