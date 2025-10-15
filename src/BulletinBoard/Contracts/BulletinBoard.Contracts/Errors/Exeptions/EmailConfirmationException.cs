namespace BulletinBoard.Contracts.Errors.Exeptions;

/// <summary>
/// Ошибка подтверждения почты.
/// </summary>
public class EmailConfirmationException : Exception
{
    /// <summary>
    /// ID пользователя
    /// </summary>
    public string UserId { get; }

    /// <inheritdoc/>
    public EmailConfirmationException(string userId, string message)
        : base(message)
    {
        UserId = userId;
    }

    /// <inheritdoc/>
    public EmailConfirmationException(string userId, string message, Exception innerException)
        : base(message, innerException)
    {
        UserId = userId;
    }
}