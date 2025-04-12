namespace Domain.Models.Wrappers;

public record PaginationList<T>
{
    public  ICollection<T> Data { get; init; } = [];
    public int PageSize { get; init; } 
    public int PageNo { get; init; }
    
}