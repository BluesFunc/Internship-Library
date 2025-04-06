using Application.QueryParams;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IUserRepository : IPaginatedRepository<User,UserQueryParams>;