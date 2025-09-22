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

        public DbSet<BulletinCategory> BulletinCategory { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            OnModelCreatingPartial(modelBuilder);
            // Настройка сущности в БД
            new BulletinCategoryEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinCategory>());
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
