using Application.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Injections;

internal static class DatabaseInjections
{
    public static IServiceCollection AddDatabase(this IServiceCollection service)
    {
        service.AddDbContext<IUnitOfWork, ApplicationDbContext>(option =>
        {
            option.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        } );

    service.AddScoped<IAuthorRepository, AuthorRepository>();
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IBookRepository, BookRepository>();
        return service;
    }
}