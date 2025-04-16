using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models.QueryParams;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Repositories.QueryBuilders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : RepositoryBase<User>(context), IUserRepository
{
    public async Task<ICollection<User>> GetPaginatedCollectionAsync(UserQueryParams filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetEntityByFilter(UserQueryParams filter, CancellationToken cancellationToken = default)
    {
        var query = context.Users.AsQueryable();
        return await new UserQueryBuilder(query)
            .ByMail(filter.Mail)
            .GetEntity();
    }
}