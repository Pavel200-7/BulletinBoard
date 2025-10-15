namespace BulletinBoard.Contracts.User.AuthDto;

/// <summary>
/// Дто для входа в систему.
/// </summary>
public class LogInDto
{
    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }
}
