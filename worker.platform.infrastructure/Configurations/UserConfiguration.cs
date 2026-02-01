using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using worker.platform.domain.Entities;

namespace worker.platform.infrastructure.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(u => u.Email)
            .HasColumnType("nvarchar(max)");

        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasOne<Role>(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);
    }
}
