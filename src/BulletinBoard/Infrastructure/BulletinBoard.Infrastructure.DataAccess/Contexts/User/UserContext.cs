﻿using BulletinBoard.Domain.Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace BulletinBoard.Infrastructure.DataAccess.Contexts.User;

public partial class UserContext : IdentityDbContext<ApplicationUser>
{
    public UserContext()
    {
    }

    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }





    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("public");
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
