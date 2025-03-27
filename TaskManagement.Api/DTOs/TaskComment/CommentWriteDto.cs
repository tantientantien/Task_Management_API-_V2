public class CommentWriteDto
{
    public string Content { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
}