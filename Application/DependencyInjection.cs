using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using Application.Behaviors;
using Application.Extensions;
using Application.Interfaces.Services;
using Application.Services;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMapster();
        services.AddMediatR(cfg=>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddSingleton<IJwtService, JwtService>(opt 
            => new JwtService(new JwtSecurityTokenHandler()));
        services.AddScoped<PasswordService>(opt => new PasswordService(SHA256.Create()));
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddAuth();
        return services;
        
    }
}