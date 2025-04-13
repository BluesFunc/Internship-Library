using Domain.Entities.Abstraction;

namespace Domain.Entities;

public class Author : Entity, IAuditableEntity
{
    private Author(){}


    public Author(string name, string surname, DateOnly birthDate, string country)
    {
        Name = name;
        Surname = surname;
        BirthDate = birthDate;
        Country = country;
    }

    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public ICollection<Book> Books { get; set; } = [];
    public string Country { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}