using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BulletinBoard.Domain.Entities.Images;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Images.EntityTypeConfigurations;

public class ImageEntityTypeConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Image");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .IsRequired();

        builder.Property(i => i.Content)
            .IsRequired();

        builder.Property(i => i.ContentType)
            .HasColumnType("varchar(255)")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(i => i.Name)
            .HasColumnType("varchar(255)")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(i => i.Length)
            .HasMaxLength(5 * 1024 * 1024)
            .IsRequired();
    }
}
