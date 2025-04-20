using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worker.platform.domain.Entities;
using worker.platform.domain.ValueObjects;

namespace worker.platform.infrastructure.Configurations;

public class JobRequestConfiguration: IEntityTypeConfiguration<JobRequest>
{
    public void Configure(EntityTypeBuilder<JobRequest> builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasOne(x => x.TaskType)
            .WithMany()
            .HasForeignKey(x => x.TaskTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId);

        builder.HasOne(x => x.City)
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.Details)
            .HasColumnType("nvarchar(max)");

        builder.OwnsMany(x => x.Params, paramsBuilder =>
        {
            paramsBuilder.ToJson(); // This ensures `Params` are stored as JSON.

            // Configure the nested list within the owned entity
            paramsBuilder.OwnsMany(p => p.JobRequestParamsValues, valuesBuilder =>
            {
                valuesBuilder.ToJson(); // This ensures `JobRequestParamsValues` are stored as JSON if needed.
            });
        });
    }
}
