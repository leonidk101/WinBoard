namespace Kanban.API.Features.TaskItems.Models;

public record NewTaskItemDto(Guid BoardListId, string Title, string Description);