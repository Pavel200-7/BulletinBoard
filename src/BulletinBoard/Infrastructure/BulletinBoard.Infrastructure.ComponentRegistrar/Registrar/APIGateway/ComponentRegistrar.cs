using BulletinBoard.AppServices.Contexts.Apigateway.Mapping;
using BulletinBoard.AppServices.Contexts.User.Mapping;
using Microsoft.Extensions.DependencyInjection;



namespace BulletinBoard.Infrastructure.ComponentRegistrar;

public static class ComponentRegistrar 
{

    public static IServiceCollection RegisterAPIGatewayServices(this IServiceCollection services)
    {
        

        return services;
    }

    public static IServiceCollection RegistrarAPIGatewayMappers(this IServiceCollection services)
    {
        services.AddAutoMapper
            (
                typeof(ApigatewayMappingProfile)
            );

        return services;
    }
}
