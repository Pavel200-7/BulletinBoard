using BulletinBoard.AppServices.Contexts.User.Mapping;
using BulletinBoard.AppServices.Contexts.User.Repository;
using BulletinBoard.AppServices.Contexts.User.Services;
using BulletinBoard.AppServices.Contexts.User.Services.IServices;
using BulletinBoard.Domain.Entities.User;
using BulletinBoard.Infrastructure.DataAccess.Contexts.User;
using BulletinBoard.Infrastructure.DataAccess.Contexts.User.UserRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace BulletinBoard.Infrastructure.ComponentRegistrar.Registrar.User;

public static class ComponentRegistrar 
{
    public static IServiceCollection RegisterUserServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserEmailConformationService, UserEmailConformationService>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }

    public static IServiceCollection RegisterUserRepositories(this IServiceCollection services)
    {
        // Базовый глупый репозиторий.
        //services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        services.AddScoped<IUserEmailConfirmationRepositoryAdapter, UserEmailConfirmationRepositoryAdapter>();
        services.AddScoped<IUserRepositoryAdapter, UserRepositoryAdapter>();

        services.AddScoped<IUnitOfWorkUser, UnitOfWorkUser>();


        return services;
    }

    public static IServiceCollection RegistrarUserMappers(this IServiceCollection services)
    {
        services.AddAutoMapper
            (
                typeof(UserMappingProfile)
            );

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
