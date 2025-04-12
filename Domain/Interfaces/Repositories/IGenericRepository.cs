using Domain.Entities.Abstraction;

namespace Domain.Interfaces.Repositories;

public interface IGenericRepository<T> where T: Entity 
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken token);
    Task<T> AddAsync(T entity, CancellationToken token);
    void Update(T entity);
    void Delete(T entity);
}