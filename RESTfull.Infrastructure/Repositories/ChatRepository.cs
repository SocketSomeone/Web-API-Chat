using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RESTfull.Domain;

namespace RESTfull.Infrastructure.Repositories;

public class ChatRepository : Repository<Chat>
{
    public ChatRepository(DataContext context) : base(context)
    {
    }

    public override Chat? Get(Expression<Func<Chat, bool>> predicate)
    {
        return _set
            .Include(c => c.Messages)
            .ThenInclude(m => m.Author)
            .FirstOrDefault(predicate);
    }

    public override IEnumerable<Chat> GetAll()
    {
        return _set
            .Include(c => c.Messages)
            .ThenInclude(m => m.Author);
    }
}