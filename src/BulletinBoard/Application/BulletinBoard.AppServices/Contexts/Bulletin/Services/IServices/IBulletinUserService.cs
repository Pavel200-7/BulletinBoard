using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Contracts.Bulletin.BulletinUser.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinUser.FilterDto;
using BulletinBoard.Contracts.Bulletin.BulletinUser.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
///  Сервис для работы со сущностью пользователя - создателя объявлений.
/// </summary>
public interface IBulletinUserService : IBaseCRUDService
    <
    BulletinUserDto,
    BulletinUserCreateDto,
    BulletinUserUpdateDto
    >
{
    /// <summary>
    /// Получить список пользователей по фильтру.
    /// </summary>
    /// <param name="userFilterDto">Формат данных для фильтрации пользователей.</param>
    /// <returns>Коллекция базового формата данных пользователя - владельца объявления.</returns>
    public Task<IReadOnlyCollection<BulletinUserDto>> GetAsync(BulletinUserFilterDto userFilterDto);

    /// <summary>
    /// Изменить имя по id.
    /// </summary>
    /// <param name="id">Id пользователя.</param>
    /// <param name="name">Новое имя пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных пользователя - владельца объявления.</returns>
    public Task<BulletinUserDto> ChangeNameAsync(Guid id, string name, CancellationToken cancellationToken);

    /// <summary>
    /// Изменить пользователя - владельца объявления.
    /// </summary>
    /// <param name="id">Id пользователя.</param>
    /// <param name="userLocationDto"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных пользователя - владельца объявления.</returns>
    public Task<BulletinUserDto> ChangeAdressAsync(Guid id, BulletinUserUpdateLocationDto userLocationDto, CancellationToken cancellationToken);

    /// <summary>
    /// Изменить телефон пользователя - владельца объявления.
    /// </summary>
    /// <param name="id">Id пользователя.</param>
    /// <param name="phone"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных пользователя - владельца объявления.</returns>
    public Task<BulletinUserDto> ChangePhoneAsync(Guid id, string phone, CancellationToken cancellationToken);

    /// <summary>
    /// Заблокировать пользователя - владельца объявления.
    /// </summary>
    /// <param name="id">Id пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных пользователя - владельца объявления.</returns>
    public Task<BulletinUserDto> BlockUserAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Разблокировать пользователя - владельца объявления.
    /// </summary>
    /// <param name="id">Id пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных пользователя - владельца объявления.</returns>
    public Task<BulletinUserDto> UnBlockUserAsync(Guid id, CancellationToken cancellationToken);
}
