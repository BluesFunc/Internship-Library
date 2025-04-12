using Application.DTOs._Account_;
using Application.Interfaces;
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
    IUnitOfWork unitOfWork,
    IPasswordService passwordService
)
    : IRequestHandler<RegisterUserCommand, Result<TokenPair>>
{
    
    public async Task<Result<TokenPair>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        
        var user = request.Adapt<User>();
        user.Password = passwordService.HashPassword(user.Password);
        await repository.AddAsync(user, cancellationToken);
        var tokenPair = jwtService.GenerateTokenPair(user);
        user.RefreshToken = tokenPair.RefreshToken;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<TokenPair>.Successful(tokenPair);
    }
}



