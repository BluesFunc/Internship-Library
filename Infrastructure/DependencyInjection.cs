using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Application.Interfaces.Services;
using Infrastructure.Injections;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDatabase();
        services.AddAuth();
        services.AddSingleton<IJwtService, JwtService>(opt 
            => new JwtService(new JwtSecurityTokenHandler()));
        services.AddScoped<IPasswordService, PasswordService>(opt => new PasswordService(SHA256.Create()));
        return services;
    }
}