using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using BulletinBoard.Contracts.User.AuthDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.User.Services.IServices;

/// <summary>
/// Сервис аутентицикции самый верхнеуровневый сервис, используемый в контроллере.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Зарегистрировать и отправить на почту ссылку ее подтверждения.
    /// </summary>
    /// <param name="createDto">данные пользователя.</param>
    /// <returns>id пользователя</returns>
    public Task<string> Register(ApplicationUserCreateDto createDto);

    /// <summary>
    /// Выдать jwt.
    /// </summary>
    /// <param name="logInDto">дто входа</param>
    /// <returns>токен доступа.</returns>
    public Task<TokenDto> LogIn(LogInDto logInDto);


    /// <summary>
    /// Подтвердить почту по id и токену подтверждения.
    /// </summary>
    /// <param name="userId">id пользователя.</param>
    /// <param name="token">Токен подтверждения почты.</param>
    /// <returns>результат операции.</returns>
    public Task<bool> ConfirmMailAsync(string userId, string token);

    /// <summary>
    /// Отправить письмо для подтверждения почты.
    /// </summary>
    /// <param name="userId">id пользователя.</param>
    /// <returns>ркзультат операции.</returns>
    public Task<bool> SendNewConfirmationEmailAsync(string userId);
}
