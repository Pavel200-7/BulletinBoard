using BulletinBoard.AppServices.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinUser
/// </summary>
public interface IBulletinUserRepository : IBaseRepository
    <
    BulletinUser,
    BulletinUserDto,
    BulletinUserCreateDto,
    BulletinUserUpdateDto
    >
{
    /// <summary>
    /// заблокировать или разблокировать пользователя.
    /// </summary>
    /// <param name="id">Id пользователя.</param>
    /// <param name="blockStatus">Статус (true - заблокировать. false - разблокировать).</param>
    /// <returns>Базовый формат пользователя или null, если его нет.</returns>
    public Task<BulletinUserDto?> SetUserBlockStatusAsync(Guid id, bool blockStatus);

    /// <summary>
    /// Поменять имя пользователя
    /// </summary>
    /// <param name="id">Id пользователя.</param>
    /// <param name="name">Новое имя.</param>
    /// <returns>Базовый формат пользователя или null, если его нет.</returns>
    public Task<BulletinUserDto?> ChangeNameAsync(Guid id, string name);

    /// <summary>
    /// Поменять местонахождение пользователя
    /// </summary>
    /// <param name="id">Id пользователя.</param>
    /// <param name="userLocationDto">Данные местонахождения.</param>
    /// <returns>Базовый формат пользователя или null, если его нет.</returns>
    public Task<BulletinUserDto?> ChangeAdressAsync(Guid id, BulletinUserUpdateLocationDto userLocationDto);

    /// <summary>
    /// Поменять телефон пользователя.
    /// </summary>
    /// <param name="id">Id пользователя.</param>
    /// <param name="phone">Новый телефон..</param>
    /// <returns>Базовый формат пользователя или null, если его нет.</returns>
    public Task<BulletinUserDto?> ChangePhoneAsync(Guid id, string phone);
}
