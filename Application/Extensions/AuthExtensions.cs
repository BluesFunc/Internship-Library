using Application.Options;
using Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application.Extensions;


internal static class AuthExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => 
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    }
                );
        services.AddAuthorizationBuilder()
            .AddPolicy("CreateAuthor", policy =>
                policy.RequireRole(UserRole.Admin.ToString())
            )
            .AddPolicy("GetBook", police =>
                police.RequireRole(
                    UserRole.Admin.ToString(), UserRole.Customer.ToString()
                ))
            .AddPolicy("UpdateBook", police =>
                police.RequireRole(UserRole.Admin.ToString()));

        return services;
    }
}