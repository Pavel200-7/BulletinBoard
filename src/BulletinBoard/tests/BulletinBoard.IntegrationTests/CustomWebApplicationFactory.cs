using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<BulletinContext>));
            if (descriptor != null) services.Remove(descriptor);

            var initializerDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(IAsyncInitializer));
            if (initializerDescriptor != null) services.Remove(initializerDescriptor);

            services.AddDbContext<BulletinContext>(options =>
            {
                options.UseNpgsql(
                    "Host=localhost;Port=5432;Database=test;Username=postgres;Password=iamdeadlytired795795",
                    b => b.MigrationsAssembly("BulletinBoard.Infrastructure.DataAccess")
                );
            });

            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<BulletinContext>();
                InitializeDatabase(db); 
            }
        });
    }

    private void InitializeDatabase(BulletinContext db)
    {
        try
        {
            db.Database.EnsureDeleted(); 
            db.Database.EnsureCreated(); 

            SeedTestData(db);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database initialization failed: {ex.Message}");
        }
    }

    private void SeedTestData(BulletinContext db)
    {

    }
}