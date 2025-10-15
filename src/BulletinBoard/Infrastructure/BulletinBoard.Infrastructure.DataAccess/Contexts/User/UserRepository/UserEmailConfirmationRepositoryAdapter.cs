using AutoMapper;
using BulletinBoard.AppServices.Contexts.User.Repository;
using BulletinBoard.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.User.UserRepository;

public class UserEmailConfirmationRepositoryAdapter : IUserEmailConfirmationRepositoryAdapter
{
    private UserManager<ApplicationUser> _userManager;
    private IMapper _autoMapper;

    public UserEmailConfirmationRepositoryAdapter
        (
        UserManager<ApplicationUser> userManager,
        IMapper autoMapper
        )
    {
        _userManager = userManager;
        _autoMapper = autoMapper;
    }

    /// <inheritdoc/>
    public async Task<bool> ConfirmMailAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);        
        var result = await _userManager.ConfirmEmailAsync(user!, token);
        return result.Succeeded;
    }

    /// <inheritdoc/>
    public async Task<string> GetNewEmailConfirmationTokenAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return await _userManager.GenerateEmailConfirmationTokenAsync(user!);
    }

    /// <inheritdoc/>
    public async Task<bool> IsEmailConfirmedAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user!.EmailConfirmed;
    }
}
