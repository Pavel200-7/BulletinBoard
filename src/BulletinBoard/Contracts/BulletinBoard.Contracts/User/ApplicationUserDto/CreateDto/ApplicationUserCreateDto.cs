namespace BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;

/// <summary>
/// Дто создания пользователя
/// </summary>
public class ApplicationUserCreateDto
{
    /// <summary>
    /// Логин
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; }

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
    /// Пароль.
    /// </summary>
    public string Password { get; set; }
}
