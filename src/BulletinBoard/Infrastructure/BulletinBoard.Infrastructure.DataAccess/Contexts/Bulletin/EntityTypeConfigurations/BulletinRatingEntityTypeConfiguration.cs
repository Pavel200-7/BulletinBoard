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

        builder.Property(r => r.UserId)
            .IsRequired();

        builder.Property(r => r.Rating)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        //builder.Property(r => r.ViewsCount)
        //    .HasColumnType("int")
        //    .HasDefaultValue(0)
        //    .IsRequired();

        builder
            .HasOne(r => r.Bulletin)
            .WithMany(b => b.Ratings)
            .HasForeignKey(r => r.BulletinId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(r => r.BulletinUser)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(r => r.BulletinId)
            .HasDatabaseName("IX_BulletinRating_BulletinId");

        builder.HasIndex(r => r.UserId)
            .HasDatabaseName("IX_BulletinRating_UserId");

        builder.HasIndex(r => r.Rating)
            .HasDatabaseName("IX_BulletinRating_Rating");



        //builder.HasIndex(r => r.ViewsCount)
        //    .HasDatabaseName("IX_BulletinRating_ViewsCount");
    }
}
