using BulletinBoard.Domain.Entities.Bulletin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

            // На случай необходимости поменять названия таблиц БД
            modelBuilder.Entity<BulletinCategory>().ToTable("BulletinCategory");

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
