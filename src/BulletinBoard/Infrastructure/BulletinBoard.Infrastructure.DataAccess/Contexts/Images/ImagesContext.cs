using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Domain.Entities.Images;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.EntityTypeConfigurations;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Images.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Images;

/// <summary>
///  Контекст для работы с сущностями домена Images.
/// </summary>
public partial class ImagesContext : DbContext
{
    public ImagesContext()
    {
    }

    public ImagesContext(DbContextOptions<ImagesContext> options)
        : base(options)
    {
    }

    public DbSet<Image> Image { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        OnModelCreatingPartial(modelBuilder);

        new ImageEntityTypeConfiguration().Configure(modelBuilder.Entity<Image>());

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
