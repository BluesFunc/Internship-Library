namespace Domain.Models.QueryParams;

public record UserQueryParams : PaginationQueryParams
{
    public string? Mail { get; init; } 
}