using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.EmailSender;

/// <summary>
/// Класс отправки почты.
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Отправить почту.
    /// </summary>
    /// <param name="toEmail">адрес электронной почты.</param>
    /// <param name="subject">Тема сообщения.</param>
    /// <param name="message">сообщение.</param>
    /// <returns></returns>
    public Task SendEmailAsync(string toEmail, string subject, string message);

}
