using MediatR;
using ToDo.Domain.Entities.Enums;

namespace ToDo.Application.TaskItems.UpdateTaskItem
{
    public class UpdateTaskItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public Status Status { get; set; }
        
        public UpdateTaskItemCommand()
        {}
        
        public UpdateTaskItemCommand(Guid id, string title, string description, int priority, Status status)
        {
            Id = id;
            Title = title;
            Description = description;
            Priority = priority;
            Status = status;
        }
    }
}