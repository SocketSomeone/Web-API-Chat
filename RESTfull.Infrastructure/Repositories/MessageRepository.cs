using RESTfull.Domain;

namespace RESTfull.Infrastructure.Repositories;

public class MessageRepository : Repository<Message>
{
    public MessageRepository(DataContext context) : base(context)
    {
    }
}