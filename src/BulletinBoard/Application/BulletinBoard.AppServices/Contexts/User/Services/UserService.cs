using BulletinBoard.AppServices.Contexts.User.Repository;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Contracts.User.ApplicationUserDto;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.User.Services;


public class UserService
{
    private IUserRepositoryAdapter _repositoryAdapter { get; set; }

    public UserService(IUserRepositoryAdapter repositoryAdapter)
    {
        _repositoryAdapter = repositoryAdapter;
    }

    /// <inheritdoc/>
    public Task<ApplicationUserDto> GetAsync(string userId)
    {
        var userDto = _repositoryAdapter.GetByIdAsync(userId);
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
    public async Task<string> ConfirmMailAsync(string userId, string token)
    {
        bool operationSucceeded = await _repositoryAdapter.ConfirmMailAsync(userId, token);
        if (!operationSucceeded)
        {
            // отправить новое письмо, выкинуть какую-то ошибку.
            throw new Exception();
        }

    }

    /// <inheritdoc/>
    public async Task<string> AddRole(Guid userId, string role, CancellationToken cancellationToken)
    {

    }

    /// <inheritdoc/>
    public Task<string> DeleteRole(Guid userId, string role, CancellationToken cancellationToken)
    {

    }

    private string GetNotFoundMessage(string id)
    {
        return $"A user with id {id} is not found";
    }
}
