using BulletinBoard.AppServices.Contexts.User.Repository;
using BulletinBoard.AppServices.Contexts.User.Services.IServices;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Contracts.User.ApplicationUserDto;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using BulletinBoard.Contracts.User.AuthDto;
using BulletinBoard.Domain.Entities.User.Enums;
using System.Data;
using System.Reflection;

namespace BulletinBoard.AppServices.Contexts.User.Services;

/// <inheritdoc/>
public class UserService : IUserService
{
    private IUserRepositoryAdapter _repositoryAdapter { get; set; }

    /// <inheritdoc/>
    public UserService
        (
        IUserRepositoryAdapter repositoryAdapter
        )
    {
        _repositoryAdapter = repositoryAdapter;
    }

    /// <inheritdoc/>
    public async Task<ApplicationUserDto> GetByIdAsync(string userId)
    {
        var userDto = await _repositoryAdapter.GetByIdAsync(userId);
        if (userDto is null) { throw new NotFoundException(GetNotFoundMessage()); }
        return userDto!;
    }

    /// <inheritdoc/>
    public async Task<ApplicationUserDto> GetByEmailAsync(string email)
    {
        var userDto = await _repositoryAdapter.GetByEmailAsync(email);
        if (userDto is null) { throw new NotFoundException(GetNotFoundMessage()); }
        return userDto!;
    }

    /// <inheritdoc/>
    public async Task<string> CreateAsync(ApplicationUserCreateDto createDto)
    {
        ApplicationUserCreateResponseDto result = await _repositoryAdapter.CreateAsync(createDto);

        if (!result.Succeeded)
        {
            throw new ValidationExeption(result.Errors);
        }

        return result.UserId;
    }

    /// <inheritdoc/>
    public async Task<bool> AddRoleAsync(string userId, string role)
    {
        ValidateRoleThrowValidationExeption(role);

        var userDto = await _repositoryAdapter.GetByIdAsync(userId);
        if (userDto is null) { throw new NotFoundException(GetNotFoundMessage()); }

        return await _repositoryAdapter.AddRoleAsync(userId, role);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteRoleAsync(string userId, string role)
    {
        ValidateRoleThrowValidationExeption(role);

        var userDto = await _repositoryAdapter.GetByIdAsync(userId);
        if (userDto is null) { throw new NotFoundException(GetNotFoundMessage()); }

        return await _repositoryAdapter.DeleteRoleAsync(userId, role);
    }

    private void ValidateRoleThrowValidationExeption(string role)
    {
        List<string> rolesInLowerCase = GetRolesList()
            .ConvertAll(role => role.ToLower());

        if (!rolesInLowerCase.Contains(role.ToLower()))
        {
            IDictionary<string, string[]> error = new Dictionary<string, string[]>() 
            { 
                { "Invalid role", new string[] { $"A role named {role} does not exist" } } 
            }; 
            throw new ValidationExeption(error);
        }
    }

    /// <inheritdoc/>
    public async Task<bool> CheckPassword(LogInDto logInDto)
    {
        return await _repositoryAdapter.CheckPassword(logInDto);
    }

    private List<string> GetRolesList()
    {
        return typeof(Roles)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(string))
            .Select(f => (string)f.GetValue(null)!)
            .ToList();
    }

    private string GetNotFoundMessage() => $"User is not found";
}
