using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LabelConfiguration : IEntityTypeConfiguration<Label>
{
    public void Configure(EntityTypeBuilder<Label> builder)
    {

        // Seed data
        builder.HasData(
            new Label { Id = 1, Name = "Urgent" },
            new Label { Id = 2, Name = "Bug" },
            new Label { Id = 3, Name = "Feature Request" },
            new Label { Id = 4, Name = "High Priority" },
            new Label { Id = 5, Name = "Low Priority" },
            new Label { Id = 6, Name = "Improvement" }
        );
    }
}