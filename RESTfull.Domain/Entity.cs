using System.ComponentModel.DataAnnotations;

namespace RESTfull.Domain;

public abstract class Entity
{
    [Key]
    public Guid Id { get; set; }
}