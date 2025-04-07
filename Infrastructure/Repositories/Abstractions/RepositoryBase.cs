using Application.Interfaces.Repositories;
using Application.QueryParams;
using Domain.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Abstractions;

public abstract class RepositoryBase<T>(DbContext context) 
    : IGenericRepository<T>
where T : Entity
{
    public async Task<T?> GetByIdAsync(Guid id)
    {
        var entity = await context.FindAsync<T>(id);
        return entity;
    }
    public async Task DeleteByIdAsync(Guid id)
    {
        await context.Set<T>().Where(x => x.Id == id).ExecuteDeleteAsync();
    }
    
    public async Task UpdateAsync(T entity)
    {
        context.Update(entity);
    }

    public async Task<T> AddAsync(T entity)
    {
        var entry = await context.AddAsync(entity);
        return entry.Entity;
    }
    
}