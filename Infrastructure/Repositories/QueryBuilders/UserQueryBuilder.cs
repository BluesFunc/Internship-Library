using Domain.Entities;

namespace Infrastructure.Repositories.QueryBuilders;

public class UserQueryBuilder(IQueryable<User> query) : QueryBuilder<User>(query)
{
    public UserQueryBuilder ByMail(string? mail)
    {
        if (mail != null)
        {
           _query = _query.Where(x => x.Mail == mail);        
        }

        return this;
    }
}




