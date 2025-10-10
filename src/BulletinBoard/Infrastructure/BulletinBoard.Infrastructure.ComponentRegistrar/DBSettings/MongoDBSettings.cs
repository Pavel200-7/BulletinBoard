namespace BulletinBoard.Infrastructure.ComponentRegistrar.DBSettings;

/// <summary>
/// Используется только для подключения драйвера MongoDB.
/// </summary>
public class MongoDBSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
}
