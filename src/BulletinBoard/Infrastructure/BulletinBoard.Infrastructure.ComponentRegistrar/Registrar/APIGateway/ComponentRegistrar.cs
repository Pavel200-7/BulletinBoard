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
using BulletinBoard.AppServices.Contexts.Images.Repository;
using BulletinBoard.AppServices.Contexts.Images.Sercices;
using BulletinBoard.AppServices.Contexts.Images.Sercices.IServices;
using BulletinBoard.AppServices.Contexts.Images.Validators.ImageValidator;
using BulletinBoard.AppServices.Contexts.Images.Validators.ImageValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.CreateDto;
using BulletinBoard.Domain.Base;
using BulletinBoard.Infrastructure.ComponentRegistrar.DBSettings;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepository;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.Mapping;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Images.ImagesRepository;
using BulletinBoard.Infrastructure.DataAccess.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;


namespace BulletinBoard.Infrastructure.ComponentRegistrar;

public static class ComponentRegistrar 
{
    public static IServiceCollection RegisterAPIGatewayServices(this IServiceCollection services)
    {
        

        return services;
    }
}
