using Application.Interfaces.Repositories;
using Domain.Abstraction;

namespace Application.Interfaces;

public interface IUnitOfWork: IDisposable
{
    Task<int> Save(CancellationToken cancellationToken);
    Task Rollback();
}