using BulletinBoard.AppServices.Contexts.Images.Mapping;
using BulletinBoard.AppServices.Contexts.Images.Repository;
using BulletinBoard.AppServices.Contexts.Images.Sercices;
using BulletinBoard.AppServices.Contexts.Images.Sercices.IServices;
using BulletinBoard.AppServices.Contexts.Images.Validators.ImageValidator;
using BulletinBoard.AppServices.Contexts.Images.Validators.ImageValidator.IValidators;
using BulletinBoard.Domain.Base;
using BulletinBoard.Infrastructure.ComponentRegistrar.DBSettings;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Images.ImagesRepository;
using BulletinBoard.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;


namespace BulletinBoard.Infrastructure.ComponentRegistrar.Registrar.Images;

public static class ComponentRegistrar
{
    public static IServiceCollection RegisterImagesServices(this IServiceCollection services)
    {
        // ImagesServices
        services.AddScoped<IImageServise, ImageServise>();

        //ImagesValidators
        services.AddScoped<IImageCreateDtoValidator, ImageCreateDtoValidator>();
        services.AddScoped<IImageValidatorFacade, ImageValidatorFacade>();

        return services;
    }

    public static IServiceCollection RegisterImagesRepositories(this IServiceCollection services)
    {
        // Базовый глупый репозиторий.
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        //ImagesRepositories
        services.AddScoped<IImageRepository, ImageRepository>();

        return services;
    }

    public static IServiceCollection RegistrarImagesMappers(this IServiceCollection services)
    {
        services.AddAutoMapper
            (
                typeof(ImagesMappingProfile)
            );

        return services;
    }

    public static IServiceCollection RegistrarImagesContexsts(this IServiceCollection services, IConfiguration configuration, string environment)
    {
        // ImageContext
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        // Регистрация маппинга для EntityBase
        if (!BsonClassMap.IsClassMapRegistered(typeof(EntityBase)))
        {
            BsonClassMap.RegisterClassMap<EntityBase>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(x => x.Id)
                  .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
            });
        }

        var mongoSettings = new MongoDBSettings();
        configuration.GetSection("MongoDB").Bind(mongoSettings);
        services.AddSingleton(mongoSettings);

        services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            var settings = serviceProvider.GetRequiredService<MongoDBSettings>(); // Без IOptions!
            return new MongoClient(settings.ConnectionString);
        });

        services.AddScoped(serviceProvider =>
        {
            var settings = serviceProvider.GetRequiredService<MongoDBSettings>(); // Без IOptions!
            var client = serviceProvider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });

        return services;
    }
}
