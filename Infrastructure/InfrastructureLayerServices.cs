using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureLayerServices
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection service)
    {
        service.AddDbContext<ApplicationDbContext>(option =>
            option.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")));
        return service;
    }
}