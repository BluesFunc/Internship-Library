using Domain.Entities.Abstraction;
using Domain.Enums;
using Domain.Exceptions.Book;

namespace Domain.Entities;

public class Book : Entity, IAuditableEntity
{
    public Book(
        string name,
        string isbn, BookGenre genre,
        string description,
        string image,
        Author author
    )
    {
        Name = name;
        Isbn = isbn;
        Genre = genre;
        Description = description;
        Image = image;
        AuthorId = author.Id;
        Author = author;
    }

    private Book(){}



    public string Name { get; set; } 
    public string Isbn { get; private set; } 
    public BookGenre Genre { get; set; }
    public string Description { get; set; } 
    public string Image { get; set; } 
    public Guid AuthorId { get; private set; }
    public Author Author { get; private set; }
    public Guid? BookedById { get; private set; }
    public User? BookedBy { get; private set; } 
    public bool IsReserved => BookedById.HasValue;
    public DateTime BookedAt { get; private set; }
    public DateTime BookingDeadline { get; private set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public void ReserveBook(User user, DateTime bookingDeadline)
    {
        if (bookingDeadline < DateTime.UtcNow)
        {
            throw new ReservationTimeException();
        }
        BookedBy = user;
        BookedById = user.Id;
        BookedAt = DateTime.UtcNow;
        BookingDeadline = bookingDeadline;
    }

    public void SetAuthor(Author author)
    {
        Author = author;
        AuthorId = author.Id;
    }
}