using Domain.Exceptions.Abstractions;

namespace Domain.Exceptions.Book;

public class ReservationTimeException()
    : DomainException("Reservation time earlier than now");