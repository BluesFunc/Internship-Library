using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.DTOs._Account_;
using Application.Interfaces.Services;
using Application.Options;
using Domain.Entities;
using Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class JwtService(JwtSecurityTokenHandler handler) : IJwtService
{
    public TokenPair GenerateTokenPair(User user)
    {
        var userRole = user.Role == UserRole.Admin ? UserRole.Admin.ToString() : UserRole.Customer.ToString();
        var userClaims = new List<Claim>()
        {
            new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole),
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString())
        };
        var accessToken = GenerateToken(TimeSpan.FromMinutes(AuthOptions.AccessLifetime), userClaims);
        var refreshToken = GenerateToken(TimeSpan.FromDays(AuthOptions.RefreshLifetime));
        return new TokenPair()
            { AccessToken = handler.WriteToken(accessToken), RefreshToken = handler.WriteToken(refreshToken) };
    }

    public bool IsTokenExpired(string encodedToken)
    {
        var token = handler.ReadJwtToken(encodedToken);
        return token.Payload.Expiration <
               (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
    }

    public JwtSecurityToken ParseToken(string token) => handler.ReadJwtToken(token);
    

    private JwtSecurityToken GenerateToken(TimeSpan tokenLifetime, List<Claim>? userClaims = null)
    {
        var token = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: userClaims,
            expires: DateTime.UtcNow.Add(tokenLifetime),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));
        return token;
    }
}