using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worker.platform.domain.Entities;

namespace worker.platform.infrastructure.Configurations;

public class JobResponseConfiguration : IEntityTypeConfiguration<JobResponse>
{
    public void Configure(EntityTypeBuilder<JobResponse> builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasOne(x => x.JobRequest).WithMany()
            .HasForeignKey(x => x.JobRequestId);

        builder.HasOne(x => x.Worker).WithMany().HasForeignKey(x => x.WorkerId);
    }
}
