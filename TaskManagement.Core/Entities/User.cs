using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
    public ICollection<TaskItem> Tasks { get; set; }
    public ICollection<TaskComment> TaskComments { get; set; }
}