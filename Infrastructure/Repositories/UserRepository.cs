using Application.Interfaces.Repositories;
using Application.QueryParams;
using Domain.Entities;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Repositories.QueryBuilders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : RepositoryBase<User>(context), IUserRepository
{
    public async Task<ICollection<User>> GetPaginatedCollectionAsync(UserQueryParams filter)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetEntityByFilter(UserQueryParams filter)
    {
        var query = context.Users.AsQueryable();
        return await new UserQueryBuilder(query)
            .ByMail(filter.Mail)
            .GetEntity();
    }
}