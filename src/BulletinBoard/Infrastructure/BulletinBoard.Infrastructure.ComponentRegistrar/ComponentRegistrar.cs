using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Mapping;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;

using BulletinBoard.Domain.Entities.Bulletin;

//using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;




namespace BulletinBoard.Infrastructure.ComponentRegistrar
{
    public static class ComponentRegistrar 
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {

            //Bulletin
            // BulletinServices
            services.AddScoped<IBulletinCategoryService, BulletinCategoryService>();


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
}
