using System.Linq.Expressions;
using Domain.Entities.Abstraction;

namespace Domain.Interfaces.Repositories;

public interface IGenericRepository<T> where T : Entity
{
    Task<bool> IsExistAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Delete(T entity);
}