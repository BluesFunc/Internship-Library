using Domain.Interfaces;
using Domain.Abstraction;

namespace Domain.Entities;

public class Author: Entity, IAuditableEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Birthdate { get; set; }
    public string Country { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}