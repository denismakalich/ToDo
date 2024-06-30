using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.TaskItems.CreateTaskItem;
using ToDo.Application.TaskItems.DeleteTaskItem;
using ToDo.Application.TaskItems.GetTaskItemById;
using ToDo.Application.TaskItems.SearchTaskItemByFilter;
using ToDo.Application.TaskItems.UpdateTaskItem;
using ToDo.Contracts.TaskItems.Request;
using ToDo.Contracts.TaskItems.Response;

namespace ToDo.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskItemController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<TaskItemController> _logger;

    public TaskItemController(IMediator mediator, IMapper mapper, ILogger<TaskItemController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost("Create", Name = "CreateTaskItem")]
    public async Task<IActionResult> Create([FromBody] CreateTaskItemRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = _mapper.Map<CreateTaskItemCommand>(request);
            await _mediator.Send(command, cancellationToken);

            _logger.LogInformation("Task item created successful");
            
            return Created();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while creating Task item.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("Get/{id:guid}", Name = "GetTaskItem")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetTaskItemByIdQuery(id);
            var taskItem = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<GetTaskItemByIdResponse>(taskItem);

            _logger.LogInformation("Task item retrieved successfully.");
            
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while getting TaskItem with Id: {TaskItemId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("Search", Name = "SearchTaskItems")]
    public async Task<IActionResult> Search([FromBody] SearchTaskItemRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = _mapper.Map<SearchTaskItemByFilterQuery>(request);
            var taskItems = await _mediator.Send(query, cancellationToken);
            var response = _mapper.Map<SearchTaskItemResponse>(taskItems);

            _logger.LogInformation("Task items retrieved successfully.");
            
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while searching Task шtems.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("Update/{id:guid}", Name = "UpdateTaskItem")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskItemRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = _mapper.Map<UpdateTaskItemCommand>(request);
            command = command with { Id = id };
            await _mediator.Send(command, cancellationToken);
            
            _logger.LogInformation("Task item updated successfully.");
            
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while updating TaskItem with Id: {TaskItemId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("Delete/{id:guid}", Name = "DeleteTaskItem")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteTaskItemCommand(id);
            await _mediator.Send(command, cancellationToken);

            _logger.LogInformation("TaskItem deleted successfully.");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting TaskItem with Id: {TaskItemId}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}