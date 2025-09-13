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

        public DbSet<BulletinCategory> BulletinCategory { get; set; }
        public DbSet<BulletinImage> BulletinImage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            OnModelCreatingPartial(modelBuilder);

            new BulletinCategoryEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinCategory>());
            new BulletinImageEntityTypeConfiguration().Configure(modelBuilder.Entity<BulletinImage>());




        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
