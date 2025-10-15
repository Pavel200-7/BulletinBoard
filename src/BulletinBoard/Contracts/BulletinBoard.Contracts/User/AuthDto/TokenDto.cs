namespace BulletinBoard.Contracts.User.AuthDto;

/// <summary>
/// ДТО с токеном аутентификации
/// </summary>
public class TokenDto
{
    /// <summary>
    /// Токен аутентификации
    /// </summary>
    public string AccessToken { get; set; }

    /// <inheritdoc/>
    public TokenDto(string accessToken)
    {
        AccessToken = accessToken;
    }
}
