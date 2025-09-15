namespace BulletinBoard.Contracts.Bulletin.BulletinUser;

/// <summary>
/// Формат данных для фильтрации пользователя - владельца объявления по
///     1. Требует доработки
/// </summary>
public class BulletinUserFilterDto
{

    /// <summary>
    /// Используется ли полное имя для отбора
    /// </summary>
    public bool IsUsedFullName { get; set; }

    /// <summary>
    /// Используется ли полное имя для отбора по частичным совпадениям
    /// </summary>
    public bool IsUsedFullNameContains { get; set; }

    /// <summary>
    /// Полное имя
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Используется ли статус блокировки для отбора
    /// </summary>
    public bool IsUsedBlocked { get; set; }

    /// <summary>
    /// Был ли заблокирован
    /// </summary>
    public bool Blocked { get; set; }

    /// <summary>
    /// Используются ли координаты для отбора для отбора
    /// </summary>
    public bool IsUsedCoordinates { get; set; }

    /// <summary>
    /// Широта (местоположение)
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Долгота (местоположение)
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Используется ли поиск на полное соответствие координат
    /// </summary>
    public bool IsUsedCoordinatesEquals { get; set; }

    /// <summary>
    /// Используется ли поиск точки в пределах радиуса
    /// </summary>
    public bool IsUsedCoordinatesCloser { get; set; }

    /// <summary>
    /// Используется ли поиск точки в пределах радиуса
    /// </summary>
    public bool IsUsedCoordinatesFarther { get; set; }

    /// <summary>
    /// Радиус (по отношению к координатам)
    /// </summary>
    public double Distance { get; set; }

    /// <summary>
    /// Используется ли поиск по адресу
    /// </summary>
    public bool IsUsedFormattedAddress { get; set; }

    /// <summary>
    /// Адрес (наименование местоположения)
    /// </summary>
    public string FormattedAddress { get; set; }

    /// <summary>
    /// Используется ли поиск по телефону
    /// </summary>
    public bool IsUsedPhone { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string Phone { get; set; }
}
