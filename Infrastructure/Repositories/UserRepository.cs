using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id)
    {
        var entity = await context.FindAsync<User>(id);
        return entity;

    }

    public async Task<List<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User> AddAsync(User entity)
    {
        var entry = await context.AddAsync(entity);
        return entry.Entity;
    }

    public async Task UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}