using AutoMapper;
using Microsoft.AspNetCore.DataProtection.Repositories;
using TaskManagement.Api.Service;


namespace TaskManagement.Api.Endpoints;

public static class TaskCommentEndpoint
{
    public static RouteGroupBuilder MapTaskCommentEndpoints(this RouteGroupBuilder group)
    {
        // Get all comment by taskid
        group.MapGet("/{taskId:int}", async (IMapper mapper, IGenericRepository<TaskComment> repository, int taskId) =>
        {
            var taskComments = await repository.FindAsync(tc => tc.TaskId == taskId, tc => tc.User);

            if (!taskComments.Any())
                return Results.NotFound();

            var commentDtos = mapper.Map<IEnumerable<CommentDataDto>>(taskComments);
            return Results.Ok(SuccessResponse<IEnumerable<CommentDataDto>>.Create(commentDtos));
        });


        // Comment to a task
        group.MapPost("/{taskId:int}", async (GetCurrentUser currentUser, IMapper mapper, IGenericRepository<TaskComment> repository, int taskId, CommentWriteDto commentDto) =>
        {
            if (commentDto is null)
                return Results.BadRequest();

            int? userId = currentUser.GetCurrentUserId();
            if (!userId.HasValue || userId.Value <= 0)
                return Results.Unauthorized();

            var taskComment = mapper.Map<TaskComment>(commentDto);
            taskComment.UserId = userId.Value;
            taskComment.TaskId = taskId;

            await repository.AddAsync(taskComment);

            return Results.NoContent();
        });


        // Delet a comment
        group.MapDelete("/{commentId:int}", async (UserPermission permission, IGenericRepository<TaskComment> repository, int commentId) =>
        {
            var comment = await repository.GetByIdAsync(commentId);
            if (comment is null)
                return Results.NotFound();

            if (!await permission.isAdminOrCreator(comment.UserId))
                return Results.Forbid();

            await repository.DeleteAsync(commentId);
            return Results.NoContent();
        });

        return group;
    }
}