using BulletinBoard.Contracts.User.ApplicationUserDto;
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
    /// Отправить письмо для подтверждения почты
    /// </summary>
    /// <param name="userDto">Данные пользователя.</param>
    /// <returns>результат операции.</returns>
    public Task<bool> SendNewConfirmationEmailAsync(ApplicationUserDto userDto);

    /// <summary>
    /// Подтвердить почту по id и токену подтверждения.
    /// </summary>
    /// <param name="userId">id пользователя.</param>
    /// <param name="token">Токен подтверждения почты.</param>
    /// <returns>результат операции.</returns>
    public Task<bool> ConfirmMailAsync(string userId, string token);

    /// <summary>
    /// Является ли почта аккаунта подтвержденной.
    /// </summary>
    /// <param name="userId">id пользователя.</param>
    /// <returns>статус подтвержденности.</returns>
    public Task<bool> IsEmailConfirmedAsync(string userId);
}
