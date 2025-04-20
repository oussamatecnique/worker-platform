using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worker.platform.domain.Entities;

namespace worker.platform.infrastructure.Configurations;

public class JobAssignmentConfiguration : IEntityTypeConfiguration<JobAssignment>
{
    public void Configure(EntityTypeBuilder<JobAssignment> builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Assignee)
            .WithMany(x => x.JobAssignments)
            .HasForeignKey(x => x.AssigneeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
