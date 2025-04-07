namespace Application.QueryParams;

public record UserQueryParams : PaginationQueryParams
{
    public string? Mail { get; init; } 
}