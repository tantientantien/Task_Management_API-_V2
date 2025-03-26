public class TaskDataDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public CategoryDataDto Category { get; set; }
    public DateTime Duedate { get; set; }
}


// public int AttachmentCount { get; set; }
// public int CommentCount { get; set; }
//public List<TaskLabelDataDto> labels { get; set; } = new();
//public UserDataDto Assignee { get; set; } = new();