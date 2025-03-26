using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TaskLabelConfiguration : IEntityTypeConfiguration<TaskLabel>
{
    public void Configure(EntityTypeBuilder<TaskLabel> builder)
    {
        builder.HasKey(tl => new { tl.TaskId, tl.LabelId });

        builder.HasOne(tl => tl.Task)
            .WithMany(t => t.TaskLabels)
            .HasForeignKey(tl => tl.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tl => tl.Label)
            .WithMany(l => l.TaskLabels)
            .HasForeignKey(tl => tl.LabelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}