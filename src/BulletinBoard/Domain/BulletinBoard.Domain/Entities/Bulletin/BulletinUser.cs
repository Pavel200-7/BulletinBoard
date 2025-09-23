using BulletinBoard.Domain.Base;


namespace BulletinBoard.Domain.Entities.Bulletin;

/// <summary>
/// Сущность пользователя - владельца объявления
/// </summary>
public class BulletinUser : EntityBase
{
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

    /// <summary>
    /// Навигационное свойство для доступа к списку объявлений
    /// </summary>
    public List<BulletinMain> Bulletins { get; set; }


    /// <summary>
    /// Навигационное свойство для доступа к списку рейтингов
    /// </summary>
    public List<BulletinRating> Ratings { get; set; }
}
