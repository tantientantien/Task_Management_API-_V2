using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class TaskPatchDto
{
    [StringLength(200, ErrorMessage = "Title length cannot exceed 200 characters.")]
    [AllowNull]
    public string Title { get; set; }

    [StringLength(1000, ErrorMessage = "Description length cannot exceed 1000 characters.")]
    [AllowNull]
    public string Description { get; set; }

    [AllowNull]
    public bool IsCompleted { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "AssigneeId must be a valid positive integer.")]
    [AllowNull]
    public int AssigneeId { get; set; }

    [DataType(DataType.DateTime)]
    [AllowNull]
    public DateTime Duedate { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be a valid positive integer.")]
    [AllowNull]
    public int CategoryId { get; set; }
}
