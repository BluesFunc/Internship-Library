using Domain.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.QueryBuilders;

public abstract class QueryBuilder<T>(IQueryable<T> query) where T : Entity
{
    public async Task<List<T>> BuildPaginatedListAsync(int pageNo, int pageSize)
    {
        query = query.Skip((pageNo - 1) * pageSize)
            .Take(pageSize);
        return await query.ToListAsync();
    }

    public  Task<T?> GetEntity()
    {
        return query.FirstOrDefaultAsync();
    }
}