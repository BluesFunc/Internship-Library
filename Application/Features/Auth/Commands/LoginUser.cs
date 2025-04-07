using Application.DTOs._Account_;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.QueryParams;
using Application.Services;
using Application.Wrappers;
using FluentValidation;
using MediatR;

namespace Application.Features.Auth.Commands;

public record LoginUserCommand : IRequest<Result<TokenPair>>
{
    public string Mail { get; init; }
    public string Password { get; init; }
}

public class LoginUserHandler
    (IUserRepository userRepository, PasswordService passwordService, IJwtService jwtService) : IRequestHandler<LoginUserCommand, Result<TokenPair>>
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

public class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidator()
    {
        
        RuleFor(x => x.Mail).
            EmailAddress()
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(x => x.Mail)
            .EmailAddress()
            .MaximumLength(50)
            .NotEmpty();
        
    }
}