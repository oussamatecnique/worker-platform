using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worker.platform.domain.Entities;

namespace worker.platform.infrastructure.Configurations;

public class ReviewConfiguration: IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId);

        builder.HasOne(x => x.Worker)
            .WithMany()
            .HasForeignKey(x => x.WorkerId);

        builder.HasOne(x => x.JobAssignment)
            .WithMany()
            .HasForeignKey(x => x.JobAssignmentId);
    }
}
