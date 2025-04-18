﻿using System.Security.Claims;
using Application.DTOs._Account_;
using Application.Extensions;
using Application.Interfaces.Services;
using Domain.Interfaces.Repositories;
using Domain.Models.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Auth.Commands.RefreshToken;



public class RefreshTokenHandler(
    IHttpContextAccessor accessor,
    IUserRepository userRepository,
    IJwtService jwtService) : IRequestHandler<RefreshTokenCommand, Result<TokenPair>>
{
    private readonly HttpContext _httpContext = accessor.HttpContext!;
    
    
    public async Task<Result<TokenPair>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var authorizationToken = _httpContext.GetAuthorizationToken();
        if (authorizationToken.IsNullOrEmpty())
        {
            return Result<TokenPair>.Failed("Authorization header is missing", ErrorTypeCode.NotAuthorized);
        }
        var token = jwtService.ParseToken(authorizationToken);
        var userId = token.Claims
            .FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
        if (jwtService.IsTokenExpired(request.RefreshToken))
        {
            return Result<TokenPair>.Failed("Token is expired", ErrorTypeCode.NotAuthorized); 
        } 
        if (userId == null)
        {
            return Result<TokenPair>.Failed("User is not authorized", ErrorTypeCode.NotAuthorized);
        }
        var user = await userRepository.GetByIdAsync((Guid.Parse(userId)), cancellationToken);
        if (user == null)
        {
            return Result<TokenPair>.Failed("User is not exists", ErrorTypeCode.NotFound);
        }
        if (user.RefreshToken != request.RefreshToken)
        {
            return Result<TokenPair>.Failed("Incorrect refresh token", ErrorTypeCode.NotAuthorized);
        }
        
        var tokenPair = jwtService.GenerateTokenPair(user);
        user.RefreshToken = tokenPair.RefreshToken;
        return Result<TokenPair>.Successful(tokenPair);

    }
}