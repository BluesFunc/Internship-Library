using Application.QueryParams;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IBookRepository : IPaginatedRepository<Book,BookQueryParams>;