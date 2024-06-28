namespace ToDo.Contracts.TaskItems.Response;

public record SearchTaskItemResponse(IEnumerable<GetTaskItemByIdResponse> TaskItems);