using System.Reflection;
using Application.Interfaces;
using Domain.Abstraction;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class ApplicationDbContext
    (DbContextOptions<ApplicationDbContext> optionsBuilder) : DbContext(optionsBuilder), IUnitOfWork
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
         
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}