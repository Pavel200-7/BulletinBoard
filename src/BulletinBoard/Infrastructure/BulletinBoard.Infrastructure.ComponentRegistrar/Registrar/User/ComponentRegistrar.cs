using BulletinBoard.AppServices.Contexts.User.Services;
using BulletinBoard.AppServices.Contexts.User.Services.IServices;
using BulletinBoard.Infrastructure.DataAccess.Contexts.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace BulletinBoard.Infrastructure.ComponentRegistrar.Registrar.User;

public static class ComponentRegistrar 
{
    public static IServiceCollection RegisterUserServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        return services;
    }

    public static IServiceCollection RegisterUserRepositories(this IServiceCollection services)
    {
        // Базовый глупый репозиторий.
        //services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        return services;
    }

    public static IServiceCollection RegistrarUserMappers(this IServiceCollection services)
    {
        //services.AddAutoMapper
        //    (
        //        typeof(BulletinMappingProfile)
        //    );

        return services;
    }

    public static IServiceCollection RegistrarUserContexsts(this IServiceCollection services, IConfiguration configuration, string environment)
    {
        // BulletinContext
        services.AddDbContext<UserContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("BulletinBoard.Infrastructure.DataAccess")
            );
        });

        return services;
    }


    public static IServiceCollection RegistrarUserInitializers(this IServiceCollection services)
    {
        services.AddAsyncInitializer<DbInitializer>();
        return services;
    }
}
