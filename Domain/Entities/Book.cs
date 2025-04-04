using Domain.Abstraction;
using Domain.Enums;

namespace Domain.Entities;

public class Book : Entity
{
    public string Name { get; set; } = null!;
    public string Isbn { get; set; } = null!;
    public BookGenre Genre { get; set; }
    public string Description { get; set; } = null!;
    public string Image { get; set; } = null!;
    public Author Author { get; set; } = null!;
    public User? BookedBy { get; set; } = null; 
    public DateTime BookedAt { get; set; }
    public DateTime BookingDeadline { get; set; }
        
}