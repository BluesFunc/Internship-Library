using Application.DTOs._Account_;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.QueryParams;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Auth.Commands.LoginUser;


public class LoginUserHandler
    (IUserRepository userRepository, IPasswordService passwordService, IJwtService jwtService) : IRequestHandler<LoginUserCommand, Result<TokenPair>>
{
    public async Task<Result<TokenPair>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var filter = new UserQueryParams() { Mail = request.Mail };
        var user = await userRepository.GetEntityByFilter(filter);
        if (user == null)
        {
            return Result<TokenPair>.Failed("User is not found");
        }

        if (user.Password != passwordService.HashPassword(request.Password))
        {
            return Result<TokenPair>.Failed("Incorrect password"); 
        }
        
        var tokenPair = jwtService.GenerateTokenPair(user);
        return Result<TokenPair>.Successful(tokenPair);

    }
}

