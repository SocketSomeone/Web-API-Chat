using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTfull.Domain;

[Table("Messages")]
public class Message : Entity
{

    public string Text { get; set; }

    public User Author { get; set; }

    public Chat Chat { get; set; }

    public DateTime Date { get; set; }
}