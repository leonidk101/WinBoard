namespace Kanban.API.Features.Tasks.Models;

public record TaskItemDto(
    Guid Id, Guid BoardListId, string Title, string? Description, int Order, 
    DateTimeOffset CreatedAt, string CreatedByUserId, uint Version
);