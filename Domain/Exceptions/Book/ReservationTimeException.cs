using Domain.Exceptions.Abstractions;

namespace Domain.Exceptions.Book;

public class ReservationTimeException : DomainException
{
    public ReservationTimeException()
    : base("Reservation time earlier than now") {}
    
}