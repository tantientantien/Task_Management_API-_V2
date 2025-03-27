using AutoMapper;
using TaskManagement.Api.Service;

namespace TaskManagement.Api.Endpoints;

public static class TaskItemEndpoint
{
    public static RouteGroupBuilder MapTaskItemEndpoints(this RouteGroupBuilder group)
    {
        // Get all tasks
        group.MapGet("/", async (IMapper mapper, IGenericRepository<TaskItem> repository) =>
        {
            var tasks = await repository.GetAllAsync();
            if (!tasks.Any())
                return Results.NoContent();

            var taskDtos = mapper.Map<IEnumerable<TaskDataDto>>(tasks);
            return Results.Ok(SuccessResponse<IEnumerable<TaskDataDto>>.Create(taskDtos));
        });

        // Get task by Id
        group.MapGet("/{id:int}", async (IMapper mapper, IGenericRepository<TaskItem> repository, int id) =>
        {
            var task = await repository.GetByIdAsync(id);
            if (task == null)
                return Results.NotFound();
            var taskDto = mapper.Map<TaskDataDto>(task);
            return Results.Ok(SuccessResponse<TaskDataDto>.Create(taskDto));
        });

        // Create a new Task
        group.MapPost("/", async (IMapper mapper, GetCurrentUser currentUser, IGenericRepository<TaskItem> repository, TaskWriteDto newTaskItem) =>
        {
            if (newTaskItem is null)
                return Results.BadRequest();

            int? userId = currentUser.GetCurrentUserId();
            if (!userId.HasValue || userId.Value <= 0)
                return Results.Unauthorized();

            var task = mapper.Map<TaskItem>(newTaskItem);
            task.UserId = userId.Value;
            
            await repository.AddAsync(task);
            var taskDto = mapper.Map<TaskDataDto>(task);
            return Results.Created($"/api/tasks/{task.Id}", SuccessResponse<TaskDataDto>.Create(taskDto));
        });

        // Update a task by Id
        group.MapPatch("/{id:int}", async (UserPermission permission, IMapper mapper, IGenericRepository<TaskItem> repository, int id, TaskPatchDto updateTaskItem) =>
        {
            if (updateTaskItem is null)
                return Results.BadRequest();

            var taskItem = await repository.GetByIdAsync(id);
            if (taskItem is null)
                return Results.NotFound();

            if(!await permission.isAdminOrCreatorOrAssignee(taskItem.UserId, taskItem.AssigneeId))
                return Results.Forbid();

            mapper.Map(updateTaskItem, taskItem);
            await repository.UpdateAsync(taskItem);
            return Results.NoContent();
        });


        // Delete a task by Id
        group.MapDelete("/{id:int}", async (UserPermission permission, IGenericRepository<TaskItem> repository, int id) =>
        {
            var task = await repository.GetByIdAsync(id);
            if (task is null)
                return Results.NotFound();

            if(!await permission.isAdminOrCreator(task.UserId))
                return Results.Forbid();

            await repository.DeleteAsync(id);
            return Results.NoContent();
        });

        return group;
    }
}