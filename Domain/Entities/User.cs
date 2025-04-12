using Domain.Entities.Abstraction;
using Domain.Enums;

namespace Domain.Entities;

public class User : Entity, IAuditableEntity
{
    public string Username { get; set; } = null!;
    public string Mail { get; set; } = null!;
    public string Password { get; set;} = null!;
    public string RefreshToken { get; set; } = null!;
    public ICollection<Book> ReservedBooks { get; set; } = [];
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}