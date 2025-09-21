using BulletinBoard.Domain.Entities.Bulletin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.EntityTypeConfigurations;

public class BulletinCategoryEntityTypeConfiguration : IEntityTypeConfiguration<BulletinCategory>
{
    public void Configure(EntityTypeBuilder<BulletinCategory> builder)
    {
        builder.ToTable("BulletinCategory");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.ParentCategoryId)
            .IsRequired(false);

        builder.Property(c => c.CategoryName)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.IsLeafy)
            .HasColumnType("boolean")
            .IsRequired()
            .HasDefaultValue(false);

        builder
            .HasMany(c => c.ChildrenCategories)
            .WithOne(c => c.ParentCategory)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Чтобы связь была явной
        builder
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.ChildrenCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(c => c.Characteristics)
            .WithOne(c => c.Category)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(c => c.Bulletins)
            .WithOne(c => c.Category)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasIndex(c => c.ParentCategoryId)
            .HasDatabaseName("IX_BulletinCategory_ParentCategoryId");

        builder.HasIndex(c => new { c.ParentCategoryId, c.CategoryName })
            .HasDatabaseName("IX_BulletinCategory_ParentId_Name")
            .IsUnique();
    }
}
