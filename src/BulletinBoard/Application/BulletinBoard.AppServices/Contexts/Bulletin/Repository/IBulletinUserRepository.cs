using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinUser
/// </summary>
public interface IBulletinUserRepository
{
    /// <summary>
    /// Получить пользователя - создателя объявления по идентификатору.
    /// </summary>
    /// <param name="id">Id пользователя - создателя объявления.</param>
    /// <returns>Базовый формат данных пользователя - владельца объявления.</returns>
    public Task<BulletinUserDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить список пользователей - создателей объявления по фильтру.
    /// </summary>
    /// <param name="specification">Расширенная спецификация для фильтрации.</param>
    /// <returns>Коллекция базового формата данных пользователя - владельца объявления.</returns>
    public Task<IReadOnlyCollection<BulletinUserDto>> FindAsync(ExtendedSpecification<BulletinUser> specification);

    /// <summary>
    /// Добавить нового пользователя - создателя объявления.
    /// </summary>
    /// <param name="userDto">Формат данных создания пользователя - владельца объявления.</param>
    /// <returns>Базовый формат данных пользователя - владельца объявления.</returns>
    public Task<BulletinUserDto> CreateAsync(BulletinUserCreateDto userDto);

    /// <summary>
    /// Обновить существующего пользователя - создателя объявления.
    /// </summary>
    /// <param name="id">Id пользователя - создателя объявления на обновление.</param>
    /// <param name="userDto">Формат данных обновления пользователя - владельца объявления.</param>
    /// <returns>Базовый формат данных пользователя - владельца объявления.</returns>
    public Task<BulletinUserDto?> UpdateAsync(Guid id, BulletinUserUpdateDto userDto);

    /// <summary>
    /// Удалить пользователя - создателя объявления по идентификатору.
    /// </summary>
    /// <param name="id">Id пользователя - создателя объявления на удаление.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    public Task SaveChangesAsync();
}
