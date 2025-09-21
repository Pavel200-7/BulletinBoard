using BulletinBoard.Domain.Entities.Bulletin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.EntityTypeConfigurations;

public class BulletinCharacteristicEntityTypeConfiguration : IEntityTypeConfiguration<BulletinCharacteristic>
{
    public void Configure(EntityTypeBuilder<BulletinCharacteristic> builder)
    {
        builder.ToTable("BulletinCharacteristic");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .HasColumnType("varchar(80)")
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(c => c.CategoryId)
            .IsRequired();

        builder
            .HasOne(c => c.Category)
            .WithMany(c => c.Characteristics)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(c => c.CharacteristicValues)
            .WithOne(c => c.Characteristic)
            .HasForeignKey(c => c.CharacteristicId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(c => c.CharacteristicСomparisons)
            .WithOne(c => c.Characteristic)
            .HasForeignKey(c => c.CharacteristicId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasIndex(c => c.CategoryId)
        .HasDatabaseName("IX_BulletinCharacteristic_CategoryId");

        builder.HasIndex(c => new { c.CategoryId, c.Name })
            .HasDatabaseName("IX_BulletinCharacteristic_CategoryId_Name")
            .IsUnique();
    }
}
