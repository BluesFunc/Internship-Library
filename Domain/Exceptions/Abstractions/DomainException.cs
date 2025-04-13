namespace Domain.Exceptions.Abstractions;

public abstract class DomainException(string message) : Exception(message);