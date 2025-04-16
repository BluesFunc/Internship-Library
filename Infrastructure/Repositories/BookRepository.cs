using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models.QueryParams;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Repositories.QueryBuilders;

namespace Infrastructure.Repositories;

public class BookRepository(ApplicationDbContext context) : RepositoryBase<Book>(context), IBookRepository
{
    public async Task<ICollection<Book>> GetPaginatedCollectionAsync(BookQueryParams filter, CancellationToken cancellationToken = default)
    {
        var query = context.Books.AsQueryable();
        return await new BookQueryBuilder(query)
            .ByAuthor(filter.AuthorId)
            .BuildPaginatedListAsync(filter.PageNo,filter.PageSize );
    }

    public async Task<Book?> GetEntityByFilter(BookQueryParams filter, CancellationToken cancellationToken = default)
    {
        var query = context.Books.AsQueryable();
        return await new BookQueryBuilder(query)
            .ByIsbn(filter.Isbn)
            .GetEntity();
    }
    
}