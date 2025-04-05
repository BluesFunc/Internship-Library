using Application.Interfaces.Repositories;
using Application.QueryParams;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    public async Task<Book?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Book>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Book> AddAsync(Book entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Book entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Book>> GetPaginatedAsync(BookQueryParams filter)
    {
        throw new NotImplementedException();
    }
}