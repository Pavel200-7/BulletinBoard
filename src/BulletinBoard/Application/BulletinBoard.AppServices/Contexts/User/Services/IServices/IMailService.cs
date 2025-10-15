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
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task SendNewConfirmationEmailAsync(string userId);
}
