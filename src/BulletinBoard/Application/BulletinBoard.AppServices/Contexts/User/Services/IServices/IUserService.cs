using BulletinBoard.Contracts.User.ApplicationUserDto;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using BulletinBoard.Contracts.User.AuthDto;
using BulletinBoard.Domain.Entities.User.Enums;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.User.Services.IServices;

/// <summary>
/// Сервис для работы с пользовалем (работы с его данными при регистрации и аутентификации).
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Получить пользователя по id.
    /// </summary>
    /// <param name="userId">Id пользователя.</param>
    /// <returns>Данные пользователя.</returns>
    public Task<ApplicationUserDto> GetByIdAsync(string userId);

    /// <summary>
    /// Получить пользователя по почте.
    /// </summary>
    /// <param name="email">Почта пользователя.</param>
    /// <returns>Данные пользователя.</returns>
    public Task<ApplicationUserDto> GetByEmailAsync(string email);

    /// <summary>
    /// Создать пользователя с отправкой на его почту ссылки на ее подтверждение.
    /// </summary>
    /// <param name="createDto">Дто создания пользователя.</param>
    /// <returns>id пользователя</returns>
    public Task<string> CreateAsync(ApplicationUserCreateDto createDto);

    /// <summary>
    /// Добавить роль.
    /// </summary>
    /// <param name="userId">id пользователя.</param>
    /// <param name="role">Роль.</param>
    /// <returns>id пользователя.</returns>
    public Task<bool> AddRoleAsync(string userId, string role);

    /// <summary>
    /// Удалить роль.
    /// </summary>
    /// <param name="userId">id пользователя.</param>
    /// <param name="role">Роль.</param>
    /// <returns>id пользователя.</returns>
    public Task<bool> DeleteRoleAsync(string userId, string role);

    /// <summary>
    /// Проверить пароль по почте.
    /// </summary>
    /// <param name="logInDto">данные входа в систему</param>
    /// <returns>результат проверки.</returns>
    public Task<bool> CheckPassword(LogInDto logInDto);
}
