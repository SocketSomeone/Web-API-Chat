using System.ComponentModel.DataAnnotations.Schema;

namespace RESTfull.Domain;

[Table("Chats")]
public class Chat : Entity
{
    public string Name { get; set; }

    public ICollection<Message> Messages { get; set; } = new List<Message>();
}