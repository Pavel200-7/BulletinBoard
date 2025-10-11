using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.CursorPaginationBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Mapping;
using BulletinBoard.AppServices.Contexts.Bulletin.Mapping.IMappingServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.CreateDto;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepository;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.Mapping;
using BulletinBoard.Infrastructure.DataAccess.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace BulletinBoard.Infrastructure.ComponentRegistrar.Registrar.Bulletin;

public static class ComponentRegistrar 
{
    public static IServiceCollection RegisterBulletinServices(this IServiceCollection services)
    {
        // BulletinServices
        services.AddScoped<IBulletinCategoryService, BulletinCategoryService>();
        services.AddScoped<IBulletinCharacteristicComparisonService, BulletinCharacteristicComparisonService>();
        services.AddScoped<IBulletinCharacteristicService, BulletinCharacteristicService>();
        services.AddScoped<IBulletinCharacteristicValueService, BulletinCharacteristicValueService>();
        services.AddScoped<IBulletinImageService, BulletinImageService>();
        services.AddScoped<IBulletinMainService, BulletinMainService>();
        services.AddScoped<IBulletinRatingService, BulletinRatingService>();
        services.AddScoped<IBulletinUserService, BulletinUserService>();
        services.AddScoped<IBulletinViewsCountService, BulletinViewsCountService>();
        services.AddScoped<IBulletinService, BulletinService>();


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
        services.AddScoped<IBulletinViewsCountSpecificationBuilder, BulletinViewsCountSpecificationBuilder>();
        services.AddScoped<IBulletinMainCursorPaginationSpecificationBuilder, BulletinMainCursorPaginationSpecificationBuilder>();

        
        // BulletinValidators
        // BulletinCategory
        services.AddScoped<IBulletinCategoryCreateDtoValidator, BulletinCategoryCreateDtoValidator>();
        services.AddScoped<IBulletinCategoryUpdateDtoValidator, BulletinCategoryUpdateDtoValidator>();
        services.AddScoped<IBulletinCategoryDeleteValidator, BulletinCategoryDeleteValidator>();
        services.AddScoped<IBulletinCategoryDtoValidatorFacade, BulletinCategoryDtoValidatorFacade>();
        // BulletinCharacteristicComparison
        services.AddScoped<IBulletinCharacteristicComparisonCreateDtoValidator, BulletinCharacteristicComparisonCreateDtoValidator>();
        services.AddScoped<IBulletinCharacteristicComparisonUpdateDtoValidator, BulletinCharacteristicComparisonUpdateDtoValidator>();
        services.AddScoped<IBulletinCharacteristicComparisonDeleteValidator, BulletinCharacteristicComparisonDeleteValidator>();
        services.AddScoped<IBulletinCharacteristicComparisonDtoValidatorFacade, BulletinCharacteristicComparisonDtoValidatorFacade>();
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
        // BulletinImage
        services.AddScoped<IBulletinImageCreateDtoValidator, BulletinImageCreateDtoValidator>();
        services.AddScoped<IBulletinImageUpdateDtoValidator, BulletinImageUpdateDtoValidator>();
        services.AddScoped<IBulletinImageDeleteValidator, BulletinImageDeleteValidator>();
        services.AddScoped<IBulletinImageDtoValidatorFacade, BulletinImageDtoValidatorFacade>();
        // BulletinMain
        services.AddScoped<IBulletinMainCreateDtoValidator, BulletinMainCreateDtoValidator>();
        services.AddScoped<IBulletinMainUpdateDtoValidator, BulletinMainUpdateDtoValidator>();
        services.AddScoped<IBulletinMainDeleteValidator, BulletinMainDeleteValidator>();
        services.AddScoped<IBulletinMainDtoValidatorFacade, BulletinMainDtoValidatorFacade>();
        services.AddScoped<IValidator<BulletinMainCreateDto>, BulletinMainCreateDtoValidator>();
        // BulletinRating
        services.AddScoped<IBulletinRatingCreateDtoValidator, BulletinRatingCreateDtoValidator>();
        services.AddScoped<IBulletinRatingUpdateDtoValidator, BulletinRatingUpdateDtoValidator>();
        services.AddScoped<IBulletinRatingDeleteValidator, BulletinRatingDeleteValidator>();
        services.AddScoped<IBulletinRatingDtoValidatorFacade, BulletinRatingDtoValidatorFacade>();
        // BulletinUser
        services.AddScoped<IBulletinUserCreateDtoValidator, BulletinUserCreateDtoValidator>();
        services.AddScoped<IBulletinUserUpdateDtoValidator, BulletinUserUpdateDtoValidator>();
        services.AddScoped<IBulletinUserDeleteValidator, BulletinUserDeleteValidator>();
        services.AddScoped<IBulletinUserDtoValidatorFacade, BulletinUserDtoValidatorFacade>();
        // BulletinViewsCount
        services.AddScoped<IBulletinViewsCountCreateDtoValidator, BulletinViewsCountCreateDtoValidator>();
        services.AddScoped<IBulletinViewsCountUpdateDtoValidator, BulletinViewsCountUpdateDtoValidator>();
        services.AddScoped<IBulletinViewsCountDeleteValidator, BulletinViewsCountDeleteValidator>();
        services.AddScoped<IBulletinViewsCountDtoValidatorFacade, BulletinViewsCountDtoValidatorFacade>();
        // Bulletin
        services.AddScoped<IBulletinCreateDtoValidator, BulletinCreateDtoValidator>();
        services.AddScoped<IBulletinUpdateDtoValidator, BulletinUpdateDtoValidator>();
        services.AddScoped<IBulletinDeleteValidator, BulletinDeleteValidator>();
        services.AddScoped<IBulletinDtoValidatorFacade, BulletinDtoValidatorFacade>();
        services.AddScoped<IValidator<BelletinCreateDto>, BulletinCreateDtoValidator>();
        services.AddScoped<IBulletinPaginationRequestDtoValidator, BulletinPaginationRequestDtoValidator>();

        return services;
    }

    public static IServiceCollection RegisterBulletinRepositories(this IServiceCollection services)
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
        services.AddScoped<IBulletinViewsCountRepository, BulletinViewsCountRepository>();
        services.AddScoped<IBulletinReposotory, BulletinReposotory>();
        services.AddScoped<IUnitOfWorkBulletin, UnitOfWorkBulletin>();

        return services;
    }

    public static IServiceCollection RegistrarBulletinMappers(this IServiceCollection services)
    {
        services.AddAutoMapper
            (
                typeof(BulletinMappingProfile)
            );

        return services;
    }

    public static IServiceCollection RegistrarBulletinContexsts(this IServiceCollection services, IConfiguration configuration, string environment)
    {
        // BulletinContext
        services.AddDbContext<BulletinContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("BulletinBoard.Infrastructure.DataAccess")
            );
        });

        return services;
    }


    public static IServiceCollection RegistrarBulletinInitializers(this IServiceCollection services)
    {
        services.AddAsyncInitializer<DbInitializer>();
        return services;
    }
}
