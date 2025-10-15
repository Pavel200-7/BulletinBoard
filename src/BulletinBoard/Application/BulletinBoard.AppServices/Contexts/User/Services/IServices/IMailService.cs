using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.User.Services.IServices;

/// <summary>
/// Сервис отправки почты.
/// </summary>
public interface IMailService
{
    /// <summary>
    /// Отправить письмо для подтверждения почты
    /// </summary>
    /// <param name="email">почта пользователя.</param>
    /// <param name="token">токен подтверждения почты.</param>
    /// <returns>ркзультат операции.</returns>
    public Task<bool> SendNewConfirmationEmailAsync(string email, string token);
}
