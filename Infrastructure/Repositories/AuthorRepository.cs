using Application.Interfaces.Repositories;
using Application.QueryParams;
using Domain.Entities;
using Infrastructure.Contexts;
using Infrastructure.Repositories.QueryBuilders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AuthorRepository(ApplicationDbContext context) : IAuthorRepository
{
    public async Task<Author?> GetByIdAsync(Guid id)
    {
        var entity = await context.FindAsync<Author>(id);
        return entity;
    }

    public async Task<List<Author>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Author> AddAsync(Author entity)
    {
        var entry = await context.AddAsync(entity);
        return entry.Entity;
    }

    public async Task UpdateAsync(Author entity)
    {

         context.Update(entity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entity =  await GetByIdAsync(id);
        context.Remove(entity);
    }

    public async Task<ICollection<Author>> GetPaginatedAsync(AuthorQueryParams filter)
    {
        var query = context.Authors.AsQueryable();
        return await new AuthorQueryBuilder(query)
            .BuildPaginatedListAsync(filter.PageNo,filter.PageSize );
    }
}