using BulletinBoard.AppServices.Contexts.Bulletin.Mapping;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;
using Microsoft.Extensions.DependencyInjection;
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
            return services;
        }

        public static IServiceCollection RegisterAppRepositories(this IServiceCollection services)
        {
            // BulletinRepositories
            services.AddScoped<IBulletinCategoryRepository, BulletinCategoryRepository>();
            services.AddScoped<IBulletinImagesRepository, BulletinImagesRepository>();
            services.AddScoped<IBulletinMainRepository, BulletinMainRepository>();
            services.AddScoped<IBulletinRatingRepository, BulletinRatingRepository>();
            services.AddScoped<IBulletinsCharacteristicName, BulletinsCharacteristicName>();
            services.AddScoped<IBulletinsCharacteristicRepository, BulletinsCharacteristicRepository>();
            services.AddScoped<IBulletinsCharacteristicValueRepository, BulletinsCharacteristicValueRepository>();

            // Репозитории следующего домена

            return services;
        }

        public static IServiceCollection RegistrarAppMappers(this IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(Program));
            return services;
        }



    }
}
