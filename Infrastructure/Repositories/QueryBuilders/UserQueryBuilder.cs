using Domain.Entities;

namespace Infrastructure.Repositories.QueryBuilders;

public class UserQueryBuilder(IQueryable<User> query) : QueryBuilder<User>(query)
{
    public UserQueryBuilder ByMail(string? mail)
    {
        if (mail != null)
        {
           Query = Query.Where(x => x.Mail == mail);        
        }

        return this;
    }
}




