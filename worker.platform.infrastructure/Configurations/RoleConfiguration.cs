using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worker.platform.domain.Entities;

namespace worker.platform.infrastructure.Configurations;

public class RoleConfiguration: IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));

        builder.HasKey(x => x.Id);
    }
}
