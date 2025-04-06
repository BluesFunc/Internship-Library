using Domain.Entities;

namespace Infrastructure.Repositories.QueryBuilders;

public class BookQueryBuilder(IQueryable<Book> query) : QueryBuilder<Book>(query)
{
    public BookQueryBuilder ByIsbn(string? isbn)
    {
        if (isbn != null)
        {
            query = query.Where(x => x.Isbn == isbn);
        }
        return this;
    }

    public BookQueryBuilder ByAuthor(Guid? id)
    {
        if (id != null)
        {
            query = query.Where(x => x.Author.Id == id);
        }

        return this;
    }
    
    
}
