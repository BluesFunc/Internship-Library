using Application.DTOs._Account_;
using Domain.Enums;
using Domain.Models.Wrappers;
using MediatR;

namespace Application.Features.Auth.Commands.RegisterUser;

public record RegisterUserCommand : IRequest<Result<TokenPair>>
{
    public string Username { get; init; }
    public string Mail { get; init; }
    public string Password { get; init; }
    public UserRole Role { get; init; }
}
