using BulletinBoard.Domain.Entities.Bulletin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.EntityTypeConfigurations;

public class BulletinMainEntityTypeConfiguration : IEntityTypeConfiguration<BulletinMain>
{
    public void Configure(EntityTypeBuilder<BulletinMain> builder)
    {
        builder.ToTable("BulletinMain");
        builder.HasKey(c => c.Id);

        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.UserId)
            .IsRequired();

        builder.Property(b => b.Title)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.Description)
            .HasColumnType("text")
            .IsRequired();

        builder.Property(b => b.CategoryId)
            .IsRequired();

        builder.Property(b => b.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();
            

        builder.Property(b => b.Hidden)
            .HasColumnType("boolean")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(b => b.Closed)
            .HasColumnType("boolean")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(b => b.Blocked)
            .HasColumnType("boolean")
            .IsRequired()
            .HasDefaultValue(false);

        builder
            .HasOne(b => b.User)
            .WithMany(u => u.Bulletins)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(b => b.Category)
            .WithMany(c => c.Bulletins)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(b => b.Characteristics)
            .WithOne(c => c.Bulletin)
            .HasForeignKey(b => b.BulletinId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
           .HasMany(b => b.Images)
           .WithOne(i => i.Bulletin)
           .HasForeignKey(i => i.BulletinId)
           .OnDelete(DeleteBehavior.Cascade);


        builder.HasIndex(b => b.UserId)
            .HasDatabaseName("IX_BulletinMain_UserId");

        builder.HasIndex(b => b.CategoryId)
            .HasDatabaseName("IX_BulletinMain_CategoryId");

        builder.HasIndex(b => b.CreatedAt)
            .HasDatabaseName("IX_BulletinMain_CreatedAt");

        builder.HasIndex(b => new { b.Hidden, b.Closed, b.Blocked })
            .HasDatabaseName("IX_BulletinMain_Visibility");
    }
}
