namespace Application.QueryParams;

public  record BookQueryParams : PaginationQueryParams
{
    public Guid? AuthorId { get; init; }
    public Guid? UserId { get; init; }
    public string? Isbn { get; init; }
}