using BulletinBoard.Domain.Entities.Bulletin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.EntityTypeConfigurations;

public class BulletinUserEntityTypeConfiguration : IEntityTypeConfiguration<BulletinUser>
{
    public void Configure(EntityTypeBuilder<BulletinUser> builder)
    {
        builder.ToTable("BulletinUser");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .IsRequired();

        builder.Property(u => u.FullName)
            .HasColumnType("varchar(128)")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(u => u.Blocked)
            .HasColumnType("boolean")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(u => u.Latitude)
            .HasColumnType("decimal(9,6)")
            .IsRequired();

        builder.Property(u => u.Longitude)
            .HasColumnType("decimal(9,6)")
            .IsRequired();

        builder.Property(u => u.FormattedAddress)
            .HasColumnType("varchar(255)")
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(u => u.Phone)
            .HasColumnType("varchar(20)")
            .HasMaxLength(20)
            .IsRequired(false);

        builder
            .HasMany(u => u.Bulletins)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(u => u.Blocked)
            .HasDatabaseName("IX_BulletinUser_Blocked");

        builder.HasIndex(u => new { u.Latitude, u.Longitude })
            .HasDatabaseName("IX_BulletinUser_Location");
    }
}
