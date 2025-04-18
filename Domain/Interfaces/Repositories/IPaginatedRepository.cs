﻿using Domain.Entities.Abstraction;
using Domain.Models.QueryParams;

namespace Domain.Interfaces.Repositories;

public interface IPaginatedRepository<TEntity, TFilter> : IGenericRepository<TEntity>
    where TEntity : Entity
    where TFilter : PaginationQueryParams 
{
    Task<ICollection<TEntity>> GetPaginatedCollectionAsync(TFilter filter);
    Task<TEntity?> GetEntityByFilter(TFilter filter);
}