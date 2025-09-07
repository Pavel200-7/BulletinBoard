using BulletinBoard.Domain.Entities.Bulletin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.EntityTypeConfigurations
{
    public class BulletinCategoryEntityTypeConfiguration : IEntityTypeConfiguration<BulletinCategory>
    {
        public void Configure(EntityTypeBuilder<BulletinCategory> builder)
        {

            builder.ToTable("BulletinCategory");

            builder.HasKey(c => c.Id);

            builder
                .HasMany(c => c.ChildrenCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
