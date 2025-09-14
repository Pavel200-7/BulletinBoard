using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin
{
    public partial class BulletinContext : DbContext
    {
        public BulletinContext()
        {
        }

        public BulletinContext(DbContextOptions<BulletinContext> options)
            : base(options)
        {
        }

        public DbSet<BulletinMain> BelletinMain { get; set; }

        public DbSet<BulletinCategory> BulletinCategory { get; set; }

        public DbSet<BulletinCharacteristic> BulletinCharacteristic { get; set; }

        public DbSet<BulletinCharacteristicValue> BulletinCharacteristicValue { get; set; }

        public DbSet<BulletinCharacteristicСomparison> BulletinCharacteristicСomparison { get; set; }

        public DbSet<BulletinImage> BulletinImage { get; set; }

        public DbSet<BulletinRating> BulletinRating { get; set; }

        public DbSet<BulletinUser> BulletinUser { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            OnModelCreatingPartial(modelBuilder);

            new BulletinMainEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinMain>());

            new BulletinCategoryEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinCategory>());

            new BulletinCharacteristicEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinCharacteristic>());

            new BulletinCharacteristicValueEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinCharacteristicValue>());

            new BulletinCharacteristicСomparisonEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinCharacteristicСomparison>());

            new BulletinImageEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinImage>());

            new BulletinRatingEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinRating>());

            new BulletinUserEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinUser>());
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
