using BulletinBoard.Domain.Entities.Bulletin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.EntityTypeConfigurations;

public class BulletinImageEntityTypeConfiguration : IEntityTypeConfiguration<BulletinImage>
{
    public void Configure(EntityTypeBuilder<BulletinImage> builder)
    {
        builder.ToTable("BulletinImage");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .IsRequired();

        builder.Property(i => i.BulletinId)
            .IsRequired();

        builder
           .HasOne(i => i.Bulletin)
           .WithMany(b => b.Images)
           .HasForeignKey(i => i.BulletinId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.Property(i => i.IsMain)
            .HasColumnType("boolean")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(i => i.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.HasIndex(i => i.BulletinId)
            .HasDatabaseName("IX_BulletinImage_BelletinId");
    }
}
