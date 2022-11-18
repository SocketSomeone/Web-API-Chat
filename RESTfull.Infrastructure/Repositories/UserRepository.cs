using RESTfull.Domain;

namespace RESTfull.Infrastructure.Repositories;

public class UserRepository : Repository<User>
{
    public UserRepository(DataContext context) : base(context)
    {
    }
}