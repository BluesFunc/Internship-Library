namespace Domain.Models.QueryParams;

public  record BookQueryParams : PaginationQueryParams
{
    public Guid? AuthorId { get; init; }
    public string? Isbn { get; init; }
}