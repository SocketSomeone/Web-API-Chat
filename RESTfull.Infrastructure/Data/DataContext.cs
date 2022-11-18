using Microsoft.EntityFrameworkCore;
using RESTfull.Domain;

namespace RESTfull.Infrastructure;

public class DataContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Message> Messages { get; set; }
    
    public virtual DbSet<Chat> Chats { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
}