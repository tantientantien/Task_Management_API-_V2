using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class TaskManagementContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public TaskManagementContext(DbContextOptions<TaskManagementContext> options) : base(options) { }
    public DbSet<Category> Categories { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<Label> Labels { get; set; }
    public DbSet<TaskLabel> TaskLabels { get; set; }
    public DbSet<TaskComment> TaskComments { get; set; }
    public DbSet<TaskAttachment> TaskAttachments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new TaskItemConfiguration());
        modelBuilder.ApplyConfiguration(new LabelConfiguration());
        modelBuilder.ApplyConfiguration(new TaskLabelConfiguration());
        modelBuilder.ApplyConfiguration(new TaskCommentConfiguration());
        modelBuilder.ApplyConfiguration(new TaskAttachmentConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }
}