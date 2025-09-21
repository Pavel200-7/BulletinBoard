using BulletinBoard.Domain.Entities.Bulletin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.EntityTypeConfigurations;

public class BulletinCharacteristicValueEntityTypeConfiguration : IEntityTypeConfiguration<BulletinCharacteristicValue>
{
    public void Configure(EntityTypeBuilder<BulletinCharacteristicValue> builder)
    {
        builder.ToTable("BulletinCharacteristicValue");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.CharacteristicId)
            .IsRequired();

        builder.Property(c => c.Value)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();  

        builder
            .HasOne(c => c.Characteristic)
            .WithMany(c => c.CharacteristicValues)
            .HasForeignKey(c => c.CharacteristicId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(c => c.CharacteristicСomparisons)
            .WithOne(c => c.CharacteristicValue)
            .HasForeignKey(c => c.CharacteristicValueId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasIndex(c => c.CharacteristicId)
        .HasDatabaseName("IX_BulletinCharacteristicValue_CharacteristicId");

        builder.HasIndex(c => new { c.CharacteristicId, c.Value })
            .HasDatabaseName("IX_BulletinCharacteristicValue_CharacteristicId_Value")
            .IsUnique();
    }
}
