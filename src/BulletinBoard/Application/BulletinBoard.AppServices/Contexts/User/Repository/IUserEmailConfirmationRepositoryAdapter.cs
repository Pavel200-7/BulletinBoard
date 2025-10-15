namespace BulletinBoard.AppServices.Contexts.User.Repository;

/// <summary>
/// Класс, ответственный за подтверждение электронной почты.
/// Является адаптером для использования asp.net identity.
/// Берет на себя всю ответственность за доступ к информации и валидацию.
/// </summary>
public interface IUserEmailConfirmationRepositoryAdapter
{
    /// <summary>
    /// Подтвердить почту по id и токену подтверждения.
    /// </summary>
    /// <param name="userId">id пользователя.</param>
    /// <param name="token">Токен подтверждения почты.</param>
    /// <returns>результат операции.</returns>
    public Task<bool> ConfirmMailAsync(string userId, string token);

    /// <summary>
    /// Получить новый токен подтверждения почты.
    /// </summary>
    /// <param name="userId">id пользователя.</param>
    /// <returns>новый токен подтверждения почты.</returns>
    public Task<string> GetNewEmailConfirmationTokenAsync(string userId);

    /// <summary>
    /// Является ли почта аккаунта подтвержденной.
    /// </summary>
    /// <param name="userId">id пользователя.</param>
    /// <returns>статус подтвержденности.</returns>
    Task<bool> IsEmailConfirmedAsync(string userId);
}
