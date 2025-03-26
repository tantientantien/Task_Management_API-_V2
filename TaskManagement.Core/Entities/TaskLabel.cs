using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TaskLabel
{
    [Key, Column(Order = 0)]
    public int TaskId { get; set; }
    [ForeignKey("TaskId")]
    public TaskItem Task { get; set; }
    [Key, Column(Order = 1)]
    public int LabelId { get; set; }
    [ForeignKey("LabelId")]
    public Label Label { get; set; }
}