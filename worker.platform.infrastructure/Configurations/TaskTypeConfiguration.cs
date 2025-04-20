using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worker.platform.domain.Entities;

namespace worker.platform.infrastructure.Configurations;

public class TaskTypeConfiguration: IEntityTypeConfiguration<domain.Entities.TaskType>
{
    public void Configure(EntityTypeBuilder<domain.Entities.TaskType> builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));
        builder.ToTable("TaskType");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Description).HasMaxLength(500);

        builder.HasOne(x => x.JobCategory)
            .WithMany()
            .HasForeignKey(x => x.JobCategoryId);

        builder.Property(x => x.Logo);
    }
}
