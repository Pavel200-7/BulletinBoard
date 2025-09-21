using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Mapping;
using BulletinBoard.AppServices.Contexts.Bulletin.Mapping.IMappingServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator.IValidators;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepository;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.Mapping;
using BulletinBoard.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BulletinBoard.Infrastructure.ComponentRegistrar;

public static class ComponentRegistrar 
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        // Bulletin - домен работы с объявлениями
        // BulletinServices
        services.AddScoped<IBulletinCategoryService, BulletinCategoryService>();
        services.AddScoped<IBulletinCharacteristicService, BulletinCharacteristicService>();
        services.AddScoped<IBulletinCharacteristicValueService, BulletinCharacteristicValueService>();
        services.AddScoped<IBulletinMainService, BulletinMainService>();
        services.AddScoped<IBulletinUserService, BulletinUserService>();


        // BulletinMappingServices
        services.AddScoped<IBulletinCategoryMappingService, BulletinCategoryMappingService>();


        // BulletinSpecificationBuilders
        services.AddScoped<IBulletinCategorySpecificationBuilder, BulletinCategorySpecificationBuilder>();
        services.AddScoped<IBulletinCharacteristicComparisonSpecificationBuilder, BulletinCharacteristicComparisonSpecificationBuilder>();
        services.AddScoped<IBulletinCharacteristicSpecificationBuilder, BulletinCharacteristicSpecificationBuilder>();
        services.AddScoped<IBulletinCharacteristicValueSpecificationBuilder, BulletinCharacteristicValueSpecificationBuilder>();
        services.AddScoped<IBulletinImageSpecificationBuilder, BulletinImageSpecificationBuilder>();
        services.AddScoped<IBulletinMainSpecificationBuilder, BulletinMainSpecificationBuilder>();
        services.AddScoped<IBulletinRatingSpecificationBuilder, BulletinRatingSpecificationBuilder>();
        services.AddScoped<IBulletinUserSpecificationBuilder, BulletinUserSpecificationBuilder>();


        // BulletinValidators
        // BulletinCategory
        services.AddScoped<IBulletinCategoryCreateDtoValidator, BulletinCategoryCreateDtoValidator>();
        services.AddScoped<IBulletinCategoryUpdateDtoValidator, BulletinCategoryUpdateDtoValidator>();
        services.AddScoped<IBulletinCategoryDeleteValidator, BulletinCategoryDeleteValidator>();
        services.AddScoped<IBulletinCategoryDtoValidatorFacade, BulletinCategoryDtoValidatorFacade>();
        // BulletinCharacteristic
        services.AddScoped<IBulletinCharacteristicCreateDtoValidator, BulletinCharacteristicCreateDtoValidator>();
        services.AddScoped<IBulletinCharacteristicUpdateDtoValidator, BulletinCharacteristicUpdateDtoValidator>();
        services.AddScoped<IBulletinCharacteristicDeleteValidator, BulletinCharacteristicDeleteValidator>();
        services.AddScoped<IBulletinCharacteristicDtoValidatorFacade, BulletinCharacteristicDtoValidatorFacade>();
        // BulletinCharacteristicValue
        services.AddScoped<IBulletinCharacteristicValueCreateDtoValidator, BulletinCharacteristicValueCreateDtoValidator>();
        services.AddScoped<IBulletinCharacteristicValueUpdateDtoValidator, BulletinCharacteristicValueUpdateDtoValidator>();
        services.AddScoped<IBulletinCharacteristicValueDeleteValidator, BulletinCharacteristicValueDeleteValidator>();
        services.AddScoped<IBulletinCharacteristicValueDtoValidatorFacade, BulletinCharacteristicValueDtoValidatorFacade>();
        // BulletinMain
        services.AddScoped<IBulletinMainCreateDtoValidator, BulletinMainCreateDtoValidator>();
        services.AddScoped<IBulletinMainUpdateDtoValidator, BulletinMainUpdateDtoValidator>();
        services.AddScoped<IBulletinMainDeleteValidator, BulletinMainDeleteValidator>();
        services.AddScoped<IBulletinMainDtoValidatorFacade, BulletinMainDtoValidatorFacade>();
        // BulletinUser
        services.AddScoped<IBulletinUserCreateDtoValidator, BulletinUserCreateDtoValidator>();
        services.AddScoped<IBulletinUserUpdateDtoValidator, BulletinUserUpdateDtoValidator>();
        services.AddScoped<IBulletinUserDeleteValidator, BulletinUserDeleteValidator>();
        services.AddScoped<IBulletinUserDtoValidatorFacade, BulletinUserDtoValidatorFacade>();

        return services;
    }

    public static IServiceCollection RegisterAppRepositories(this IServiceCollection services)
    {
        // Базовый глупый репозиторий.
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

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
