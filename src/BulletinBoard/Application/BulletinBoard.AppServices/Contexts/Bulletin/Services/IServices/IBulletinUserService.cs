using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
///  Сервис для работы со сущностью пользователя - создателя объявлений.
/// </summary>
public interface IBulletinUserService
{
    /// <summary>
    /// Получить пользователя по id.
    /// </summary>
    /// <param name="id">id пользователя.</param>
    /// <returns>Базовый формат данных пользователя - владельца объявления.</returns>
    public Task<BulletinUserDto> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить список пользователей по фильтру.
    /// </summary>
    /// <param name="userDto">Формат данных для фильтрации пользователей.</param>
    /// <returns>Коллекция базового формата данных пользователя - владельца объявления.</returns>
    public Task<IReadOnlyCollection<BulletinUserDto>> GetAsync(BulletinUserFilterDto userDto);

    /// <summary>
    /// Добавить пользователя.
    /// </summary>
    /// <param name="userDto">Формат данных создания пользователя - владельца объявления.</param>
    /// <returns>Базовый формат данных пользователя - владельца объявления.</returns>
    public Task<BulletinUserDto> CreateAsync(BulletinUserCreateDto userDto);

    /// <summary>
    /// Изменить имя по id.
    /// </summary>
    /// <param name="id">Id пользователя.</param>
    /// <param name="name">Новое имя пользователя.</param>
    /// <returns>Базовый формат данных пользователя - владельца объявления.</returns>
    public Task<BulletinUserDto> ChangeNameAsync(Guid id, string name);

    /// <summary>
    /// Изменить пользователя - владельца объявления.
    /// </summary>
    /// <param name="id">Id пользователя.</param>
    /// <param name="userLocationDto"></param>
    /// <returns>Базовый формат данных пользователя - владельца объявления.</returns>
    public Task<BulletinUserDto> ChangeAdressAsync(Guid id, BulletinUserUpdateLocationDto userLocationDto);

    /// <summary>
    /// Изменить телефон пользователя - владельца объявления.
    /// </summary>
    /// <param name="id">Id пользователя.</param>
    /// <param name="phone"></param>
    /// <returns>Базовый формат данных пользователя - владельца объявления.</returns>
    public Task<BulletinUserDto> ChangePhoneAsync(Guid id, string phone);

    /// <summary>
    /// Заблокировать пользователя - владельца объявления.
    /// </summary>
    /// <param name="id">Id пользователя.</param>
    /// <returns>Базовый формат данных пользователя - владельца объявления.</returns>
    public Task<BulletinUserDto> BlockUserAsync(Guid id);

    /// <summary>
    /// Разблокировать пользователя - владельца объявления.
    /// </summary>
    /// <param name="id">Id пользователя.</param>
    /// <returns>Базовый формат данных пользователя - владельца объявления.</returns>
    public Task<BulletinUserDto> UnBlockUserAsync(Guid id);

    /// <summary>
    /// Удалить пользователя - владельца объявления.
    /// </summary>
    /// <param name="id">Id пользователя.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);

}
