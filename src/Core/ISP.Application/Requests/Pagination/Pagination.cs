namespace ISP.Application.Requests.Pagination;

public record Pagination
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}