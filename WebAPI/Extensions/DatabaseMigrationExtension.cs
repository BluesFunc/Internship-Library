using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions;

public static class DatabaseMigrationExtension
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await MigrateAsync(dbContext);
    }

    private static async Task MigrateAsync(DbContext dbContext)
    {
            await dbContext.Database.MigrateAsync();
    }
}