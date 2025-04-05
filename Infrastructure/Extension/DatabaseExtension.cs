using Application.Interfaces;
using Application.Interfaces.Repositories;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Extension;

internal static class DatabaseExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection service )
    {
        service.AddDbContext<IUnitOfWork, ApplicationDbContext>(option =>
            option.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")));
        service.AddScoped<IAuthorRepository, AuthorRepository>();
        service.AddScoped<IBookRepository, BookRepository>();
        return service;
    }
}