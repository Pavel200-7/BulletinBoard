namespace BulletinBoard.Contracts.Bulletin.BulletinUser;

/// <summary>
/// Формат данных обновления пользователя - владельца объявления
/// </summary>
public class BulletinUserUpdateDto
{
    /// <summary>
    /// Полное имя
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Широта (местоположение)
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Долгота (местоположение)
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Адрес (наименование местоположения)
    /// </summary>
    public string? FormattedAddress { get; set; }
}
