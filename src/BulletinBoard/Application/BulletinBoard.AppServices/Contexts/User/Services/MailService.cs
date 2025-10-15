using BulletinBoard.AppServices.Contexts.User.Services.IServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.User.Services;

/// <summary>
/// Он ничего не делает, но он очень полезный.
/// </summary>
public class MailService : IMailService
{
    ILogger<MailService> _logger;

    public MailService(ILogger<MailService> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> SendNewConfirmationEmailAsync(string email, string token)
    {
        _logger.LogInformation("Я типо что-то отправил, верьте мне))).");
        return true;
    }
}
