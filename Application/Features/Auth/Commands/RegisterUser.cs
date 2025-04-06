using System.Net.Mail;
using Application.DTOs._Account_;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.Features.Auth.Commands;

public record RegisterUserCommand : IRequest<Result<TokenPair>>
{
    public string Username { get; init; }
    public string Mail { get; init; }
    public string Password { get; init; }
    public UserRole Role { get; init; }
}

public class RegisterUserHandler(
    IUserRepository repository,
    IJwtService jwtService,
    IUnitOfWork unitOfWork
)
    : IRequestHandler<RegisterUserCommand, Result<TokenPair>>
{
    public async Task<Result<TokenPair>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.Adapt<User>();
        await repository.AddAsync(user);
        var tokenPair = jwtService.GenerateTokenPair(user);
        user.RefreshToken = tokenPair.RefreshToken;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<TokenPair>.Successful(tokenPair);
    }
}

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Role).IsInEnum();
        RuleFor(x => x.Mail).
            EmailAddress()
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(x => x.Username).MaximumLength(30).NotEmpty();
        
    }
}

