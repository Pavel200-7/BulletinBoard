using BulletinBoard.AppServices.Contexts.User.Repository;
using BulletinBoard.AppServices.Contexts.User.Services.IServices;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Contracts.User.ApplicationUserDto;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using BulletinBoard.Domain.Entities.User.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.User.Services;

/// <inheritdoc/>
public class UserService
{
    private IUserRepositoryAdapter _repositoryAdapter { get; set; }
    private IMailService _mailService { get; set; }

    /// <inheritdoc/>
    public UserService
        (
        IUserRepositoryAdapter repositoryAdapter,
        IMailService mailService
        )
    {
        _repositoryAdapter = repositoryAdapter;
        _mailService = mailService;
    }

    /// <inheritdoc/>
    public async Task<ApplicationUserDto> GetAsync(string userId)
    {
        var userDto = await _repositoryAdapter.GetByIdAsync(userId);
        if (userDto is null) { throw new NotFoundException(GetNotFoundMessage(userId)); }
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
    public async Task<bool> ConfirmMailAsync(string userId, string token)
    {
        var userDto = await _repositoryAdapter.GetByIdAsync(userId);
        if (userDto is null) { throw new NotFoundException(GetNotFoundMessage(userId)); }

        bool operationSucceeded = await _repositoryAdapter.ConfirmMailAsync(userId, token);
        if (!operationSucceeded)
        {
            string email = userDto.Email;
            string newToken = 

            await _mailService.SendNewConfirmationEmailAsync(email);
            throw new EmailConfirmationException(userId, GetEmailConfirmationExceptionMessage(userId));
        }

        return operationSucceeded;
    }

    /// <inheritdoc/>
    public async Task<bool> AddRoleAsync(string userId, string role, CancellationToken cancellationToken)
    {
        ValidateRoleThrowValidationExeption(role);

        var userDto = await _repositoryAdapter.GetByIdAsync(userId);
        if (userDto is null) { throw new NotFoundException(GetNotFoundMessage(userId)); }

        return await _repositoryAdapter.AddRoleAsync(userId, role);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteRoleAsync(string userId, string role, CancellationToken cancellationToken)
    {
        ValidateRoleThrowValidationExeption(role);

        var userDto = await _repositoryAdapter.GetByIdAsync(userId);
        if (userDto is null) { throw new NotFoundException(GetNotFoundMessage(userId)); }

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

    private List<string> GetRolesList()
    {
        return typeof(Roles)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(string))
            .Select(f => (string)f.GetValue(null)!)
            .ToList();
    }

    private string GetNotFoundMessage(string id) => $"A user with id {id} is not found";

    private string GetEmailConfirmationExceptionMessage(string id) => $"Email confirmation failed for user {id}. New confirmation email sent.";
}
