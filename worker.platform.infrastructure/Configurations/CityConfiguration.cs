using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worker.platform.domain.Entities;

namespace worker.platform.infrastructure.Configurations;

public class CityConfiguration: IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));

        builder.HasKey(c => c.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasOne(x => x.Region)
            .WithMany()
            .HasForeignKey(x => x.RegionId);
    }
}
