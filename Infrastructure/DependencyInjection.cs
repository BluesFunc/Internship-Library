using Infrastructure.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service)
    {
        service.AddDatabase();
        return service;
    }
}