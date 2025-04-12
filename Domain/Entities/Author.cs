using Domain.Entities.Abstraction;

namespace Domain.Entities;

public class Author : Entity, IAuditableEntity
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public ICollection<Book> Books { get; set; } = [];
    public string Country { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}