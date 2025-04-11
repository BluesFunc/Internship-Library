using Application.Wrappers;
using Domain.Abstraction;

namespace Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T: Entity 
{
    Task<T?> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    void Update(T entity);
    Task DeleteByIdAsync(Guid id);
}