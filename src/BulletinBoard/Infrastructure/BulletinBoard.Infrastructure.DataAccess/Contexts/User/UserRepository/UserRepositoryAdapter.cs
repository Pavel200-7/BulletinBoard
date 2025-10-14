using AutoMapper;
using BulletinBoard.AppServices.Contexts.User.Repository;
using BulletinBoard.Contracts.User.ApplicationUserDto;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using BulletinBoard.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.User.UserRepository;

public class UserRepositoryAdapter : IUserRepositoryAdapter
{
    private UserManager<ApplicationUser> _userManager;
    private IMapper _autoMapper;

    public UserRepositoryAdapter
        (
        UserManager<ApplicationUser> userManager,
        IMapper autoMapper
        )
    {
        _userManager = userManager;
        _autoMapper = autoMapper;
    }

    /// <inheritdoc/>
    public async Task<ApplicationUserDto?> GetByIdAsync(string userId)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId);
        if (user is null) { return null; }
        return _autoMapper.Map<ApplicationUserDto>(user);
    }

    /// <inheritdoc/>
    public async Task<ApplicationUserDto?> GetByUserNameAsync(string username)
    {
        ApplicationUser? user = await _userManager.FindByNameAsync(username);
        if (user is null) { return null; }
        return _autoMapper.Map<ApplicationUserDto>(user);
    }

    /// <inheritdoc/>
    public async Task<ApplicationUserDto?> GetByEmailAsync(string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if (user is null) { return null; }
        return _autoMapper.Map<ApplicationUserDto>(user);
    }

    /// <inheritdoc/>
    public async Task<ApplicationUserCreateResponseDto> CreateAsync(ApplicationUserCreateDto createDto)
    {
        ApplicationUser userData = _autoMapper.Map<ApplicationUser>(createDto);
        string password = createDto.Password;
        var result = await _userManager.CreateAsync(userData, password);

        string userId = userData.Id;
        bool succeeded = result.Succeeded;
        IDictionary<string, string[]> errors = result.Errors
            .Select(e => (e.Code, new string[] { e.Description }))
            .ToDictionary();

        return new ApplicationUserCreateResponseDto(userId, succeeded, errors);
    }

    /// <inheritdoc/>
    public async Task<bool> ConfirmMailAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        var result = await _userManager.ConfirmEmailAsync(user, token);
        return result.Succeeded;
    }

    /// <inheritdoc/>
    public async Task<bool> AddRole(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        var result = await _userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteRole(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        var result = await _userManager.RemoveFromRoleAsync(user, role);
        return result.Succeeded;
    }
}
