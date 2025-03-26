using AutoMapper;


public static class TaskItemEndpoint
{
    public static RouteGroupBuilder MapTaskItemEndpoints(this RouteGroupBuilder group)
    {
        group.RequireAuthorization();

        // Get all tasks
        group.MapGet("/", async (IGenericRepository<TaskItem> repository) =>
                await repository.GetAllAsync());

        // Get task by Id
        group.MapGet("/{id:int}", async (IMapper mapper, IGenericRepository<TaskItem> repository, int id) =>
        {
            var task = await repository.GetByIdAsync(id);
            return task is not null ? Results.Ok(mapper.Map<TaskDataDto>(task)) : Results.NotFound();
        });

        // Create a new Task
        group.MapPost("/", async (IMapper mapper, GetCurrentUser currentUser, IGenericRepository<TaskItem> repository, TaskWriteDto newTaskItem) =>
        {
            if (newTaskItem is null)
                return Results.BadRequest();
            var task = mapper.Map<TaskItem>(newTaskItem);
            task.UserId = currentUser.GetCurrentUserId();
            await repository.AddAsync(task);
            return Results.Created($"/api/tasks/{task.Id}", task);
        });

        // Delete a task by Id
        group.MapPatch("/{id:int}", async (IMapper mapper, IGenericRepository<TaskItem> repository, int id, TaskPatchDto updateTaskItem) =>
        {
            var taskItem = await repository.GetByIdAsync(id);
            if (taskItem is null)
                return Results.NotFound();
            mapper.Map(updateTaskItem, taskItem);
            await repository.UpdateAsync(taskItem);
            return Results.NoContent();
        });


        group.MapDelete("/{id:int}", async (IGenericRepository<TaskItem> repository, int id) =>
        {
            var game = await repository.GetByIdAsync(id);
            if (game is null)
                return Results.NotFound();

            await repository.DeleteAsync(id);
            return Results.NoContent();
        });
        return group;
    }
}

