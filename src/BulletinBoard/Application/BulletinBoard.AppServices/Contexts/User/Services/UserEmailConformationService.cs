using BulletinBoard.AppServices.Contexts.User.Repository;
using BulletinBoard.AppServices.Contexts.User.Services.IServices;
using BulletinBoard.AppServices.Repository;
using BulletinBoard.Contracts.Errors.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.User.Services;

/// <inheritdoc/>
public class UserEmailConformationService : IUserEmailConformationService
{
    private readonly IUserRepositoryAdapter _repositoryAdapter;
    private readonly IUserEmailConfirmationRepositoryAdapter _emailConfirmationRepositoryAdapter;
    private IUnitOfWorkUser _unitOfWork;

    /// <inheritdoc/>
    public UserEmailConformationService
        (
        IUserRepositoryAdapter repositoryAdapter,
        IUserEmailConfirmationRepositoryAdapter emailConfirmationRepositoryAdapter,
        IUnitOfWorkUser unitOfWorkUser
        )
    {
        _repositoryAdapter = repositoryAdapter;
        _emailConfirmationRepositoryAdapter = emailConfirmationRepositoryAdapter;
        _unitOfWork = unitOfWorkUser;
    }

    /// <inheritdoc/>
    public async Task<bool> ConfirmMailAsync(string userId, string token)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var userDto = await _repositoryAdapter.GetByIdAsync(userId);
            if (userDto is null) { throw new NotFoundException(GetNotFoundMessage(userId)); }

            bool operationSucceeded = await _emailConfirmationRepositoryAdapter.ConfirmMailAsync(userId, token);
            if (!operationSucceeded)
            {
                throw new EmailConfirmationException(userId, GetEmailConfirmationExceptionMessage(userId));
            }

            await _unitOfWork.CommitTransactionAsync();

            return operationSucceeded;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw ex;
        }
    }

    /// <inheritdoc/>
    public async Task<string> GetNewEmailConfirmationTokenAsync(string userId)
    {
        return await _emailConfirmationRepositoryAdapter.GetNewEmailConfirmationTokenAsync(userId);
    }

    /// <inheritdoc/>
    public async Task<bool> IsEmailConfirmedAsync(string userId)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var userDto = await _repositoryAdapter.GetByIdAsync(userId);
            if (userDto is null) { throw new NotFoundException(GetNotFoundMessage(userId)); }
            await _unitOfWork.CommitTransactionAsync();
            return await _emailConfirmationRepositoryAdapter.IsEmailConfirmedAsync(userId);
        } catch (Exception ex) {
            await _unitOfWork.RollbackTransactionAsync();
            throw ex;
        }
    }

    private string GetNotFoundMessage(string id) => $"A user with id {id} is not found";

    private string GetEmailConfirmationExceptionMessage(string id) => $"Email confirmation failed for user {id}. New confirmation email sent.";
}
