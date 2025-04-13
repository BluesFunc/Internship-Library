using Application.DTOs._Account_;
using Application.Interfaces.Requests;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand : IRequest<Result<TokenPair>>, ITransactionRequest
{
    public string RefreshToken { get; init; }
    
}