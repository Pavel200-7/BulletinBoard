using BulletinBoard.AppServices.Contexts.User.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.User.UserRepository;

public class UserEmailConfirmationRepositoryAdapter : IUserEmailConfirmationRepositoryAdapter
{
    /// <inheritdoc/>
    public Task<bool> ConfirmMailAsync(string userId, string token);

    /// <inheritdoc/>
    public Task<string> GetNewEmailConfirmationTokenAsync(string userId);

    /// <inheritdoc/>
    Task<bool> IsEmailConfirmedAsync(string userId);
}
