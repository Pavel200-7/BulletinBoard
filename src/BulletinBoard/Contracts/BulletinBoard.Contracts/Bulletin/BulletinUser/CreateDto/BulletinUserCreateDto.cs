namespace BulletinBoard.Contracts.Bulletin.BulletinUser.CreateDto;

/// <summary>
/// Формат данных создания пользователя - владельца объявления
/// </summary>
public class BulletinUserCreateDto
{
    /// <summary>
    /// Id пользователя - владельца объявления
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Полное имя
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Был ли заблокирован
    /// </summary>
    public bool Blocked { get; set; }

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

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string Phone { get; set; }
}
