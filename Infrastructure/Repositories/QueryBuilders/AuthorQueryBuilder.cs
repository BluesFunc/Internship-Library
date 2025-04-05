using Domain.Entities;

namespace Infrastructure.Repositories.QueryBuilders;

public class AuthorQueryBuilder(IQueryable<Author> query) : QueryBuilder<Author>(query)
{
    
}