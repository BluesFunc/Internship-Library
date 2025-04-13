using Domain.Entities.Abstraction;
using Domain.Enums;

namespace Domain.Entities;

public class User : Entity, IAuditableEntity
{
    private User()
    {
    }

    public User(
        string username,
        string mail,
        UserRole role)
    {
        Username = username;
        Mail = mail;
        Role = role;
    }

    public string Username { get; set; } = null!;
    public string Mail { get; private set; } = null!;
    public string Password { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public ICollection<Book> ReservedBooks { get; private set; } = [];
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    
}