using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worker.platform.domain.Entities;

namespace worker.platform.infrastructure.Configurations;

public class TaskTypeParamsDefinitionConfig: IEntityTypeConfiguration<TaskTypeParamsDefinition>
{
    public void Configure(EntityTypeBuilder<TaskTypeParamsDefinition> builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.TaskType)
            .WithMany()
            .HasForeignKey(x => x.TaskTypeId);
    }
}
