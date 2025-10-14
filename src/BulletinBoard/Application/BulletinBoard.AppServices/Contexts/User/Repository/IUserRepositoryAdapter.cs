using BulletinBoard.Contracts.User.ApplicationUserDto;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.User.Repository;

/// <summary>
/// Класс для работы с сущностью ApplicationUser.
/// Является адаптером для использования asp.net identity.
/// Берет на себя всю ответственность за доступ к информации и валидацию.
/// </summary>
public interface IUserRepositoryAdapter
{
    /// <summary>
    /// Получить пользователя по id.
    /// </summary>
    /// <param name="userId">Id пользователя.</param>
    /// <returns>Данные пользователя.</returns>
    public Task<ApplicationUserDto?> GetByIdAsync(string userId);

    /// <summary>
    /// Получить пользователя по имени.
    /// </summary>
    /// <param name="username">Имя пользователя.</param>
    /// <returns>Данные пользователя.</returns>
    public Task<ApplicationUserDto?> GetByUserNameAsync(string username);

    /// <summary>
    /// Получить пользователя по имени.
    /// </summary>
    /// <param name="email">Почта пользователя.</param>
    /// <returns>Данные пользователя.</returns>
    public Task<ApplicationUserDto?> GetByEmailAsync(string email);

    /// <summary>
    /// Создать пользователя с отправкой на его почту ссылки на ее подтверждение.
    /// </summary>
    /// <param name="createDto">Дто создания пользователя.</param>
    /// <returns>id пользователя</returns>
    public Task<ApplicationUserCreateResponseDto> CreateAsync(ApplicationUserCreateDto createDto);

    /// <summary>
    /// Подтвердить почту по id и токену подтверждения.
    /// </summary>
    /// <param name="userId">id пользователя.</param>
    /// <param name="token">Токен подтверждения почты.</param>
    /// <returns>результат операции.</returns>
    public Task<bool> ConfirmMailAsync(string userId, string token);

    /// <summary>
    /// Добавить роль.
    /// </summary>
    /// <param name="userId">id пользователя.</param>
    /// <param name="role">Роль.</param>
    /// <returns>результат операции.</returns>
    public Task<bool> AddRole(string userId, string role);

    /// <summary>
    /// Удалить роль.
    /// </summary>
    /// <param name="userId">id пользователя.</param>
    /// <param name="role">Роль.</param>
    /// <returns>результат операции.</returns>
    public Task<bool> DeleteRole(string userId, string role);
}
