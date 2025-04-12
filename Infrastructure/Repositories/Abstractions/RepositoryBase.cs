using System.Runtime.Intrinsics.X86;
using Domain.Entities.Abstraction;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Abstractions;

public abstract class RepositoryBase<T>(DbContext context) 
    : IGenericRepository<T>
where T : Entity
{
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var entity = await context.Set<T>().FirstOrDefaultAsync(x=>x.Id == id);
        return entity;
    }
    public void Delete (T entity)
    {
        context.Set<T>().Remove(entity);
    }
    
    public void  Update(T entity)
    {
        context.Set<T>().Update(entity);
    }
    

    public async Task<T> AddAsync(T entity, CancellationToken token = default)
    {
        var entry = await context.Set<T>().AddAsync(entity);
        return entry.Entity;
    }
    
}