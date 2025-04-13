using Application.DTOs._Account_;
using Application.Interfaces.Services;
using Domain.Interfaces.Repositories;
using Domain.Models.QueryParams;
using Domain.Models.Wrappers;
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
            return Result<TokenPair>.Failed("User is not found", ErrorTypeCode.NotFound);
        }

        if (user.Password != passwordService.HashPassword(request.Password))
        {
            return Result<TokenPair>.Failed("Incorrect password or email", ErrorTypeCode.NotAuthorized); 
        }
        
        var tokenPair = jwtService.GenerateTokenPair(user);
        user.RefreshToken = tokenPair.RefreshToken;
        
        return Result<TokenPair>.Successful(tokenPair);

    }
}

