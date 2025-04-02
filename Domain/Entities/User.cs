using Domain.Abstraction;
using Domain.Interfaces;

namespace Domain.Entities;

public class User: Entity, IAuditableEntity
{
    public string Username { get; set; } = null!;
    public string Mail { get; set; } = null!;
    public string Password { get; set;} = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}