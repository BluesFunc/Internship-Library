using System.ComponentModel.DataAnnotations;

namespace Application.Wrappers;

public record PaginationList<T>
{
    public  ICollection<T> Data { get; init; } = [];
    public int PageSize { get; init; } 
    public int PageNo { get; init; }
    
}