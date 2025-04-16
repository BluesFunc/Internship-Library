using Domain.Entities.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.QueryBuilders;

public abstract class QueryBuilder<T>(IQueryable<T> query) where T : Entity
{
    protected  IQueryable<T> Query = query;
    public async Task<List<T>> BuildPaginatedListAsync(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        Query = Query.Skip((pageNo - 1) * pageSize)
            .Take(pageSize);
        return await Query.ToListAsync(cancellationToken);
    }

    public  Task<T?> GetEntity( CancellationToken cancellationToken = default)
    {
        return Query.FirstOrDefaultAsync(cancellationToken);
    }
}