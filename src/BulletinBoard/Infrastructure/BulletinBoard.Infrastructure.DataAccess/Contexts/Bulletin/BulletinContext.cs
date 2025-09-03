using BulletinBoard.Domain.Entities.Bulletin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin
{
    public class BulletinContext : DbContext
    {
        public BulletinContext(DbContextOptions<BulletinContext> options) : base(options)
        {
        }

        public DbSet<BelletinMain> BelletinMain { get; set; }
        public DbSet<BulletinCategory> BulletinCategory { get; set; }
        public DbSet<BulletinCharacteristic> BulletinCharacteristic { get; set; }
        public DbSet<BulletinCharacteristicName> BulletinCharacteristicName { get; set; }
        public DbSet<BulletinCharacteristicValue> BulletinCharacteristicValue { get; set; }
        public DbSet<BulletinImages> BulletinImages { get; set; }
        public DbSet<BulletinRating> BulletinRating { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // На случай необходимости поменять названия таблиц БД
            modelBuilder.Entity<BelletinMain>().ToTable("BelletinMain");
            modelBuilder.Entity<BulletinCategory>().ToTable("BulletinCategory");
            modelBuilder.Entity<BulletinCharacteristic>().ToTable("BulletinCharacteristic");
            modelBuilder.Entity<BulletinCharacteristicName>().ToTable("BulletinCharacteristicName");
            modelBuilder.Entity<BulletinCharacteristicValue>().ToTable("BulletinCharacteristicValue");
            modelBuilder.Entity<BulletinImages>().ToTable("BulletinImages");
            modelBuilder.Entity<BulletinRating>().ToTable("BulletinRating");
        }
    }
}
