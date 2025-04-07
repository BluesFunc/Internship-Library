using Application.Interfaces.Repositories;
using Domain.Abstraction;

namespace Application.Interfaces;

public interface IUnitOfWork: IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}