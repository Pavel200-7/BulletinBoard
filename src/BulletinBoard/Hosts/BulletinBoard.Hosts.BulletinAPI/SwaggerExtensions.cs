/// <summary>
/// Класс для настройки свагера
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Метод для подключения документации.
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwaggerWithXmlComments(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            var basePath = AppContext.BaseDirectory;

            var xmlPathMain = Path.Combine(basePath, "BulletinBoard.Hosts.Api.xml");
            options.IncludeXmlComments(xmlPathMain);

            var xmlPathContracts = Path.Combine(basePath, "BulletinBoard.Contracts.xml");
            if (File.Exists(xmlPathContracts))
            {
                options.IncludeXmlComments(xmlPathContracts);
            }
        });
    }
}