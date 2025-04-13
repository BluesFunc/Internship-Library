using Application.DTOs._Account_;
using Application.Interfaces.Requests;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models.Wrappers;
using Mapster;
using MediatR;

namespace Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserHandler(
    IUserRepository repository,
    IJwtService jwtService,
    IPasswordService passwordService
)
    : IRequestHandler<RegisterUserCommand, Result<TokenPair>>
{
    public async Task<Result<TokenPair>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await repository.IsExistAsync(x => x.Mail == request.Mail, cancellationToken))
        {
            return Result<TokenPair>.Failed("Given email already in use", ErrorTypeCode.EntityConflict);
        }

        var user = new User(mail: request.Mail, username: request.Username, role: request.Role);
        user.Password = passwordService.HashPassword(request.Password);
        await repository.AddAsync(user, cancellationToken);
        var tokenPair = jwtService.GenerateTokenPair(user);
        user.RefreshToken = tokenPair.RefreshToken;
        return Result<TokenPair>.Successful(tokenPair);
    }
}