namespace BulletinBoard.Contracts.User.ApplicationUserDto;

/// <summary>
/// Дто пользователя
/// </summary>
public class ApplicationUserDto 
{
    /// <summary>
    /// Id пользователя.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Логин
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Подтверждена ли почта.
    /// </summary>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Номер телефон
    /// </summary>
    public string PhoneNumber { get; set; }

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
    /// Роли
    /// </summary>
    public List<string> Roles { set; get; }
}
