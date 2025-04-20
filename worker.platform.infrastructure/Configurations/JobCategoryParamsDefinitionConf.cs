using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worker.platform.domain.Entities;

namespace worker.platform.infrastructure.Configurations;

public class JobCategoryParamsDefinitionConf: IEntityTypeConfiguration<TaskTypeParamsDefinition>
{
    public void Configure(EntityTypeBuilder<TaskTypeParamsDefinition> builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));

        builder.HasOne(x => x.TaskType)
            .WithMany()
            .HasForeignKey(x => x.TaskTypeId);
    }
}
