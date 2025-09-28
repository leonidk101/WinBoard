namespace Kanban.Web;

public record ApiClientOptions
{
    public required string BaseUrl { get; init; }
}
