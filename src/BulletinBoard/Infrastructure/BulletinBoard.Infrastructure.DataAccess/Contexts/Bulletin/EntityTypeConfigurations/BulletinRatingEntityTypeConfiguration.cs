using BulletinBoard.Domain.Entities.Bulletin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.EntityTypeConfigurations;

public class BulletinRatingEntityTypeConfiguration : IEntityTypeConfiguration<BulletinRating>
{
    public void Configure(EntityTypeBuilder<BulletinRating> builder)
    {
        builder.ToTable("BulletinRating");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .ValueGeneratedOnAdd();

        builder.Property(r => r.BulletinId)
            .IsRequired();

        builder.Property(r => r.Rating)
            .HasColumnType("decimal(3,2)")
            .IsRequired();

        builder.Property(r => r.ViewsCount)
            .HasColumnType("int")
            .HasDefaultValue(0)
            .IsRequired();

        builder
            .HasOne(r => r.Bulletin)
            .WithOne(b => b.Rating)
            .HasForeignKey<BulletinRating>(r => r.BulletinId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasIndex(r => r.BulletinId)
            .HasDatabaseName("IX_BulletinRating_BulletinId")
            .IsUnique();

        builder.HasIndex(r => r.Rating)
            .HasDatabaseName("IX_BulletinRating_Rating");

        builder.HasIndex(r => r.ViewsCount)
            .HasDatabaseName("IX_BulletinRating_ViewsCount");
    }
}
