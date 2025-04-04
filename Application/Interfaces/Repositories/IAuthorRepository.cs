using Application.QueryParams;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IAuthorRepository: IPaginatedRepository<Author, AuthorQueryParams>;