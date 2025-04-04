namespace Application.QueryParams;

public abstract record PaginationQueryParams
{
    public int PageNo { get; init; } = 1;
    public int PageSize { get; init; } = 5;
}


