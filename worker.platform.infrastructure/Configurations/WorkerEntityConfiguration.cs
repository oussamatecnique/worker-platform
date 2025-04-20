using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worker.platform.domain.Entities;

namespace worker.platform.infrastructure.Configurations;

public class WorkerEntityConfiguration: IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasIndex(x => x.CIN)
            .IsUnique();

        builder.HasOne<JobCategory>(x => x.JobCategory)
            .WithMany()
            .HasForeignKey(x => x.JobCategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.City)
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
