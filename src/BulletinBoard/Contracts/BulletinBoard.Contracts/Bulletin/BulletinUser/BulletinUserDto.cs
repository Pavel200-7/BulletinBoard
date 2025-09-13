namespace BulletinBoard.Contracts.Bulletin.BulletinUser;

/// <summary>
/// Базовый формат данных пользователя - владельца объявления.
/// Сама сущность является копией основной сущности User, которая 
/// создается следом за созданием аккаунта и выполняет функции изоляции
/// доменов.
/// </summary>
public class BulletinUserDto
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
}
