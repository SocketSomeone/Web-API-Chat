using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTfull.Domain;

[Table("Users")]
public class User : Entity
{
    public string Username { get; set; }
    
    public string Password { get; set; }
}