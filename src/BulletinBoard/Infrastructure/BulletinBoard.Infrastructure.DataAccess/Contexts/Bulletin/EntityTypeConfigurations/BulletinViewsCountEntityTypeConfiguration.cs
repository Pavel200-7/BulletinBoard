using BulletinBoard.Domain.Entities.Bulletin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.EntityTypeConfigurations;

public class BulletinViewsCountEntityTypeConfiguration : IEntityTypeConfiguration<BulletinViewsCount>
{
    public BulletinViewsCountEntityTypeConfiguration()
    {
    }

    public void Configure(EntityTypeBuilder<BulletinViewsCount> builder)
    {
        builder.ToTable("BulletinViewsCount");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .ValueGeneratedOnAdd();

        builder.Property(v => v.BulletinId)
            .IsRequired();

        builder.Property(v => v.ViewsCount)
            .HasColumnType("int")
            .IsRequired();

        builder
            .HasOne(v => v.Bulletin)
            .WithOne(b => b.ViewsCount)
            .HasForeignKey<BulletinViewsCount>(v => v.BulletinId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(v => v.BulletinId)
            .HasDatabaseName("IX_BulletinViewsCount_BulletinId");
    }
}


