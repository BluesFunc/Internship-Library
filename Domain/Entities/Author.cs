using Domain.Interfaces;
using Domain.Abstraction;

namespace Domain.Entities;

public class Author: Entity, IAuditableEntity
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Birthdate { get; set; } = null!;
    public string Country { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}