using BulletinBoard.Contracts.User.ApplicationUserDto;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
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
    public Task<ApplicationUserDto> GetAsync(string userId);

    /// <summary>
    /// Создать пользователя с отправкой на его почту ссылки на ее подтверждение.
    /// </summary>
    /// <param name="createDto">Дто создания пользователя.</param>
    /// <returns>id пользователя</returns>
    public Task<string> CreateAsync(ApplicationUserCreateDto createDto);

    /// <summary>
    /// Подтвердить почту по id.
    /// </summary>
    /// <param name="userId">id пользователя.</param>
    /// <returns>id пользователя.</returns>
    public Task<bool> ConfirmMailAsync(string userId);

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
}
