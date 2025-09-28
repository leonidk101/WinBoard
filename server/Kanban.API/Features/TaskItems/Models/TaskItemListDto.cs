namespace Kanban.API.Features.TaskItems.Models;

public record TaskItemListDto(Guid Id, string Title, string? Description, int Order, DateTimeOffset CreatedAt);