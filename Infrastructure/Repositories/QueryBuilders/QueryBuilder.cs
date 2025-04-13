using Domain.Entities.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.QueryBuilders;

public abstract class QueryBuilder<T>(IQueryable<T> query) where T : Entity
{
    protected  IQueryable<T> _query = query;
    public async Task<List<T>> BuildPaginatedListAsync(int pageNo, int pageSize)
    {
        _query = _query.Skip((pageNo - 1) * pageSize)
            .Take(pageSize);
        return await _query.ToListAsync();
    }

    public  Task<T?> GetEntity()
    {
        return _query.FirstOrDefaultAsync();
    }
}