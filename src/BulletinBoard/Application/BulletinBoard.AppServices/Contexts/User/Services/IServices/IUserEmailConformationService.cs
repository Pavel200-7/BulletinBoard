using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.User.Services.IServices;

/// <summary>
/// Сервис для работы со статусом подтверждения почты.
/// </summary>
public interface IUserEmailConformationService
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
    /// <returns>новый токен подтверждения почты или null, если пользователя нет.</returns>
    public Task<string> GetNewEmailConfirmationTokenAsync(string userId);

    /// <summary>
    /// Является ли почта аккаунта подтвержденной.
    /// </summary>
    /// <param name="userId">id пользователя.</param>
    /// <returns>статус подтвержденности.</returns>
    public Task<bool> IsEmailConfirmedAsync(string userId);
}
