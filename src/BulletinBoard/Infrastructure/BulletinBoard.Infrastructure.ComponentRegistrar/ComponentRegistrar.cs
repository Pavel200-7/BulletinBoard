using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Mapping.MappingServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Mapping.MappingServices.IMappingServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepository;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BulletinBoard.Infrastructure.ComponentRegistrar;

public static class ComponentRegistrar 
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {

        // Bulletin
        // BulletinServices
        services.AddScoped<IBulletinCategoryService, BulletinCategoryService>();
        services.AddScoped<IBulletinUserService, BulletinUserService>();


        // BulletinMappingServices
        services.AddScoped<IBulletinCategoryMappingService, BulletinCategoryMappingService>();

        // BulletinSpecificationBuilders
        services.AddScoped<IBulletinCategorySpecificationBuilder, BulletinCategorySpecificationBuilder>();
        services.AddScoped<IBulletinUserSpecificationBuilder, BulletinUserSpecificationBuilder>();


        // BulletinValidators
        services.AddScoped<IBulletinCategoryCreateDtoValidator, BulletinCategoryCreateDtoValidator>();
        services.AddScoped<IBulletinCategoryUpdateDtoValidator, BulletinCategoryUpdateDtoValidator>();
        services.AddScoped<IBulletinCategoryDtoValidatorFacade, BulletinCategoryDtoValidatorFacade>();

        return services;
    }

    public static IServiceCollection RegisterAppRepositories(this IServiceCollection services)
    {
        // BulletinRepositories
        services.AddScoped<IBulletinCategoryRepository, BulletinCategoryRepository>();
        services.AddScoped<IBulletinCharacteristicComparisonRepository, BulletinCharacteristicComparisonRepository>();
        services.AddScoped<IBulletinCharacteristicRepository, BulletinCharacteristicRepository>();
        services.AddScoped<IBulletinCharacteristicValueRepository, BulletinCharacteristicValueRepository>();
        services.AddScoped<IBulletinImageRepository, BulletinImageRepository>();
        services.AddScoped<IBulletinMainRepository, BulletinMainRepository>();
        services.AddScoped<IBulletinRatingRepository, BulletinRatingRepository>();
        services.AddScoped<IBulletinUserRepository, BulletinUserRepository>();


       
        // Репозитории следующего домена

        return services;
    }

    public static IServiceCollection RegistrarAppMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(
            cnf => { },
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
