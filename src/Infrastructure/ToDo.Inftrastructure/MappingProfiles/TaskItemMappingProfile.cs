using AutoMapper;
using ToDo.Application.Models.TaskItems;
using ToDo.Application.TaskItems.CreateTaskItem;
using ToDo.Application.TaskItems.DeleteTaskItem;
using ToDo.Application.TaskItems.GetTaskItemById;
using ToDo.Application.TaskItems.SearchTaskItemByFilter;
using ToDo.Application.TaskItems.UpdateTaskItem;
using ToDo.Contracts.TaskItems.Request;
using ToDo.Contracts.TaskItems.Response;
using ToDo.Domain.Entities;

namespace ToDo.Inftrastructure.MappingProfiles;

public class TaskItemMappingProfile : Profile
{
    public TaskItemMappingProfile()
    {
        CreateMap<CreateTaskItemRequest, CreateTaskItemCommand>()
            .ConstructUsing(src =>
                new CreateTaskItemCommand(src.Title, src.Description, src.Priority, src.Status, src.UserId));

        CreateMap<SearchTaskItemRequest, SearchTaskItemFilter>();

        CreateMap<SearchTaskItemRequest, SearchTaskItemByFilterQuery>()
            .ForMember(dest => dest.Filter,
                opt => opt.MapFrom(src =>
                    new SearchTaskItemFilter(src.Page, src.PageSize, src.Status, src.SortBy)))
            .ConvertUsing(src =>
                new SearchTaskItemByFilterQuery(
                    new SearchTaskItemFilter(src.Page, src.PageSize, src.Status, src.SortBy)));

        CreateMap<IEnumerable<TaskItem>, SearchTaskItemResponse>()
            .ConvertUsing((src, _, context) =>
            {
                var taskItems = src.Select(e => context.Mapper.Map<GetTaskItemByIdResponse>(e));
                return new SearchTaskItemResponse(taskItems);
            });

        CreateMap<UpdateTaskItemRequest, UpdateTaskItemCommand>();

        CreateMap<DeleteTaskItemRequest, DeleteTaskItemCommand>();

        CreateMap<GetTaskItemByIdResponse, GetTaskItemByIdQuery>()
            .ConstructUsing(src => new GetTaskItemByIdQuery(src.Id));

        CreateMap<TaskItem, GetTaskItemByIdResponse>();

        CreateMap<string, DateOnly>()
            .ConstructUsing(src => DateOnly.Parse(src))
            .ReverseMap()
            .ConstructUsing(src => src.ToString("yyyy-MM-dd"));
    }
}