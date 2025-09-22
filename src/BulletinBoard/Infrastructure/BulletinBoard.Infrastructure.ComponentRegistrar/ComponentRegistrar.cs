using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders;
using BulletinBoard.AppServices.Contexts.Bulletin.Mapping;
using BulletinBoard.AppServices.Contexts.Bulletin.Mapping.IMappingServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.Mapping;
using BulletinBoard.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BulletinBoard.Infrastructure.ComponentRegistrar;

/// <summary>
/// Регистрация в DI контейнер
/// </summary>
public static class ComponentRegistrar 
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        // Bulletin - домен работы с объявлениями
        // BulletinServices
        services.AddScoped<IBulletinCategoryService, BulletinCategoryService>();



        // BulletinMappingServices
        services.AddScoped<IBulletinCategoryMappingService, BulletinCategoryMappingService>();


        // BulletinSpecificationBuilders
        services.AddScoped<IBulletinCategorySpecificationBuilder, BulletinCategorySpecificationBuilder>();


        // BulletinValidators
        // BulletinCategory
        services.AddScoped<IBulletinCategoryCreateDtoValidator, BulletinCategoryCreateDtoValidator>();
        services.AddScoped<IBulletinCategoryUpdateDtoValidator, BulletinCategoryUpdateDtoValidator>();
        services.AddScoped<IBulletinCategoryDeleteValidator, BulletinCategoryDeleteValidator>();
        services.AddScoped<IBulletinCategoryDtoValidatorFacade, BulletinCategoryDtoValidatorFacade>();


        return services;
    }

    public static IServiceCollection RegisterAppRepositories(this IServiceCollection services)
    {
        // Базовый глупый репозиторий.
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        // BulletinRepositories
        services.AddScoped<IBulletinCategoryRepository, BulletinCategoryRepository>();

        // Репозитории следующего домена

        return services;
    }

    public static IServiceCollection RegistrarAppMappers(this IServiceCollection services)
    {
        services.AddAutoMapper
            (
                typeof(BulletinMappingProfile)
            // Другие профайлеры
            );

        return services;
    }

    public static IServiceCollection RegistrarAppContexsts(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<BulletinContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("BulletinBoard.Infrastructure.DataAccess")
            );
        });

        // Другие контексты

        return services;
    }


    public static IServiceCollection RegistrarAppInitializers(this IServiceCollection services)
    {
        services.AddAsyncInitializer<DbInitializer>();

        //  Другие инициализаторы

        return services;
    }
}
