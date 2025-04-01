using Domain.Abstraction;
using Domain.Interfaces;

namespace Domain.Entities;

public class User: Entity, IAuditableEntity
{
    public string Username { get; set; }
    public string Mail { get; set; }
    public string Password { get; set;}
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}