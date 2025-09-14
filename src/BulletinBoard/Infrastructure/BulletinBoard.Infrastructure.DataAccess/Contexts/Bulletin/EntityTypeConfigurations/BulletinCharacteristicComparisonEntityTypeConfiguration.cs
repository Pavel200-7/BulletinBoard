using BulletinBoard.Domain.Entities.Bulletin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.EntityTypeConfigurations;

public class BulletinCharacteristicComparisonEntityTypeConfiguration : IEntityTypeConfiguration<BulletinCharacteristicComparison>
{
    public void Configure(EntityTypeBuilder<BulletinCharacteristicComparison> builder)
    {
        builder.ToTable("BulletinCharacteristicComparison");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.BulletinId) 
            .IsRequired();

        builder.Property(c => c.CharacteristicId)
            .IsRequired();

        builder.Property(c => c.CharacteristicValueId)
            .IsRequired();

        builder
            .HasOne(c => c.Bulletin)
            .WithMany(b => b.Characteristics)
            .HasForeignKey(c => c.BulletinId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(c => c.Characteristic)
            .WithMany(c => c.CharacteristicСomparisons)
            .HasForeignKey(c => c.CharacteristicId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(c => c.CharacteristicValue)
            .WithMany(c => c.CharacteristicСomparisons)
            .HasForeignKey(c => c.CharacteristicValueId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => c.BulletinId)
        .HasDatabaseName("IX_BulletinCharacteristicComparison_BulletinId");

        builder.HasIndex(c => c.CharacteristicId)
            .HasDatabaseName("IX_BulletinCharacteristicComparison_CharacteristicId");

        builder.HasIndex(c => c.CharacteristicValueId)
            .HasDatabaseName("IX_BulletinCharacteristicComparison_CharacteristicValueId");

        builder.HasIndex(c => new { c.BulletinId, c.CharacteristicId })
            .HasDatabaseName("IX_BulletinCharacteristicComparison_BulletinId_CharacteristicId")
            .IsUnique();
    }
}
