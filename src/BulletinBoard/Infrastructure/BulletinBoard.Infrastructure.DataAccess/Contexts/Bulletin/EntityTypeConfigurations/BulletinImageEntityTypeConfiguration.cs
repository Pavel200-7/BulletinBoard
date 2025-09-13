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
            .ValueGeneratedOnAdd();

        builder.Property(i => i.BelletinId)
            .IsRequired();

        builder.Property(i => i.IsMain)
            .HasColumnType("bool")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(i => i.Name)
            .HasColumnType("varchar(255)")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(i => i.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(i => i.Path)
            .HasColumnType("varchar(500)") 
            .HasMaxLength(500)
            .IsRequired();

        builder.HasIndex(i => i.BelletinId)
            .HasDatabaseName("IX_BulletinImage_BelletinId");
    }
}
