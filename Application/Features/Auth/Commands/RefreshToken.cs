using System.Security.Claims;
using Application.DTOs._Account_;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Auth.Commands;

public record RefreshTokenCommand : IRequest<Result<TokenPair>>
{
    public string RefreshToken { get; init; }
}

public class RefreshTokenHandler(
    IHttpContextAccessor accessor,
    IUserRepository userRepository,
    IJwtService jwtService,
    IUnitOfWork unitOfWork) : IRequestHandler<RefreshTokenCommand, Result<TokenPair>>
{
    private readonly HttpContext _httpContext = accessor.HttpContext!;

    public async Task<Result<TokenPair>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.User.Claims
            .FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
        if (jwtService.IsTokenExpired(request.RefreshToken))
        {
            return Result<TokenPair>.Failed("Token is expired"); 
        } 
        if (userId == null)
        {
            return Result<TokenPair>.Failed("User is not authorized");
        }
        var user = await userRepository.GetByIdAsync((Guid.Parse(userId)));
        if (user == null)
        {
            return Result<TokenPair>.Failed("User is not exists");
        }
        if (user.RefreshToken != request.RefreshToken)
        {
            return Result<TokenPair>.Failed("Incorrect refresh token");
        }
        
        var tokenPair = jwtService.GenerateTokenPair(user);
        user.RefreshToken = tokenPair.RefreshToken;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<TokenPair>.Successful(tokenPair);

    }
}