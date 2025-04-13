using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models.QueryParams;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Repositories.QueryBuilders;

namespace Infrastructure.Repositories;

public class AuthorRepository(ApplicationDbContext context) : RepositoryBase<Author>(context), IAuthorRepository
{
    
    public async Task<ICollection<Author>> GetPaginatedCollectionAsync(AuthorQueryParams filter)
    {
        var query = context.Authors.AsQueryable();
        return await new AuthorQueryBuilder(query)
            .BuildPaginatedListAsync(filter.PageNo,filter.PageSize );
    }

    public async Task<Author?> GetEntityByFilter(AuthorQueryParams filter)
    {
        var query = context.Authors.AsQueryable();
        return await new AuthorQueryBuilder(query).GetEntity();
    }
}