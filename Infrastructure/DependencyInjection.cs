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
        services.AddScoped<JwtSecurityTokenHandler>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<SHA256>();
        services.AddScoped<IPasswordService, PasswordService>();
        return services;
    }
}