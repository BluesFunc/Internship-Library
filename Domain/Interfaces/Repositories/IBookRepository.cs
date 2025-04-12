using Domain.Entities;
using Domain.Models.QueryParams;

namespace Domain.Interfaces.Repositories;

public interface IBookRepository : IPaginatedRepository<Book,BookQueryParams>;