using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;


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

        public DbSet<BulletinMain> BulletinMain { get; set; }

        public DbSet<BulletinCategory> BulletinCategory { get; set; }

        public DbSet<BulletinCharacteristic> BulletinCharacteristic { get; set; }

        public DbSet<BulletinCharacteristicValue> BulletinCharacteristicValue { get; set; }

        public DbSet<BulletinCharacteristicComparison> BulletinCharacteristicСomparison { get; set; }

        public DbSet<BulletinImage> BulletinImage { get; set; }

        public DbSet<BulletinRating> BulletinRating { get; set; }

        public DbSet<BulletinUser> BulletinUser { get; set; }

        public DbSet<BulletinViewsCount> BulletinViewsCount { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            OnModelCreatingPartial(modelBuilder);

            new BulletinMainEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinMain>());

            new BulletinCategoryEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinCategory>());

            new BulletinCharacteristicEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinCharacteristic>());

            new BulletinCharacteristicValueEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinCharacteristicValue>());

            new BulletinCharacteristicComparisonEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinCharacteristicComparison>());

            new BulletinImageEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinImage>());

            new BulletinRatingEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinRating>());

            new BulletinUserEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinUser>());

            new BulletinViewsCountEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinViewsCount>());

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
