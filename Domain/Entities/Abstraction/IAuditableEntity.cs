namespace Domain.Entities.Abstraction;

public interface  IAuditableEntity 
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}