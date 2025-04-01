using Application.Interfaces.Repositories;
using Domain.Abstraction;

namespace Application.Interfaces;

public interface IUnitOfWork: IDisposable
{
    IGenericRepository<T> Repository<T>() where T : Entity;
    Task<int> Save(CancellationToken cancellationToken);
    Task Rollback();
}