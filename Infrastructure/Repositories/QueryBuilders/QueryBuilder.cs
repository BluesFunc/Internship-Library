using Domain.Entities.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.QueryBuilders;

public abstract class QueryBuilder<T>(IQueryable<T> query) where T : Entity
{
    protected  IQueryable<T> Query = query;
    public async Task<List<T>> BuildPaginatedListAsync(int pageNo, int pageSize)
    {
        Query = Query.Skip((pageNo - 1) * pageSize)
            .Take(pageSize);
        return await Query.ToListAsync();
    }

    public  Task<T?> GetEntity()
    {
        return Query.FirstOrDefaultAsync();
    }
}