using Application.Wrappers;
using Domain.Abstraction;

namespace Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T: Entity 
{
    Task<T> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteByIdAsync(Guid id);
}