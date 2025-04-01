using Domain.Abstraction;
using Domain.Enums;

namespace Domain.Entities;

public class Book: Entity
{
    public string Name { get; set; }
    public BookGenre Genre { get; set; }
    public string Description { get; set; }
    public Author Author { get; set; }
    public DateTime BookedAt { get; set; }
    public DateTime BookingDeadline { get; set; }
        
}