using BulletinBoard.AppServices.Contexts.User.Repository;
using BulletinBoard.AppServices.Contexts.User.Services.IServices;
using BulletinBoard.AppServices.Repository;
using BulletinBoard.Contracts.Errors.Exeptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BulletinBoard.AppServices.EmailSender;
using BulletinBoard.Contracts.User.ApplicationUserDto;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace BulletinBoard.AppServices.Contexts.User.Services;

/// <inheritdoc/>
public class UserEmailConformationService : IUserEmailConformationService
{
    private readonly IUserRepositoryAdapter _repositoryAdapter;
    private readonly IUserEmailConfirmationRepositoryAdapter _emailConfirmationRepositoryAdapter;
    private IEmailSender _emailSender;
    private IUnitOfWorkUser _unitOfWork;
    private IConfiguration _configuration;
    private ILogger<UserEmailConformationService> _logger;

    /// <inheritdoc/>
    public UserEmailConformationService
        (
        IUserRepositoryAdapter repositoryAdapter,
        IUserEmailConfirmationRepositoryAdapter emailConfirmationRepositoryAdapter,
        IUnitOfWorkUser unitOfWorkUser,
        IEmailSender emailSender,
        IConfiguration configuration,
        ILogger<UserEmailConformationService> logger
        )
    {
        _repositoryAdapter = repositoryAdapter;
        _emailConfirmationRepositoryAdapter = emailConfirmationRepositoryAdapter;
        _unitOfWork = unitOfWorkUser;
        _emailSender = emailSender;
        _configuration = configuration;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> SendNewConfirmationEmailAsync(ApplicationUserDto userDto)
    {
        string emailConfirmationToken = await GetNewEmailConfirmationTokenAsync(userDto.Id);


        string domainPath = _configuration.GetSection("ApiGatewayPath").Value;

        string callbackUrl = $"http://{domainPath}/api/auth/confirm_email?userId={userDto.Id}&token={WebUtility.UrlEncode(emailConfirmationToken)}";

        string subject = "Email confirm";
        await _emailSender.SendEmailAsync(userDto.Email, subject, callbackUrl);

        _logger.LogInformation("Сообщение для подтверждения почты отправленно.");
        return true;
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
            _logger.LogInformation($"Почта пользователя с id {userId} была подтверждена.");

            return operationSucceeded;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw ex;
        }
    }

    /// <inheritdoc/>
    private async Task<string> GetNewEmailConfirmationTokenAsync(string userId)
    {
        string newToken = await _emailConfirmationRepositoryAdapter.GetNewEmailConfirmationTokenAsync(userId);
        _logger.LogInformation($"Для пользователя с id {userId} был сгенерирован новый токен подтверждения почты.");
        return newToken;
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
