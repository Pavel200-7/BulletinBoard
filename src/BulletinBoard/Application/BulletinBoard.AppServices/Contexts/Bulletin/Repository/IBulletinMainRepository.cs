using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinMain
/// </summary>
public interface IBulletinMainRepository
{
    /// <summary>
    /// Получить объявления по идентификатору.
    /// </summary>
    /// <param name="id">Id объявления.</param>
    /// <returns>Базовый формат данных объявления.</returns>
    public Task<BulletinMainDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить список с объявлениями по фильтру.
    /// </summary>
    /// <param name="specification">Расширенная спецификация для фильтрации.</param>
    /// <returns>Коллекция данных базового формата данных объявления.</returns>
    public Task<IReadOnlyCollection<BulletinMainDto>> FindAsync(ExtendedSpecification<BulletinMain> specification);

    /// <summary>
    /// Добавить новое объявление.
    /// </summary>
    /// <param name="bulletinDto">Формат данных создания объявления.</param>
    /// <returns>Базовый формат данных объявления.</returns>
    public Task<BulletinMainDto> CreateAsync(BulletinMainCreateDto bulletinDto);

    /// <summary>
    /// Обновить существующее объявление.
    /// </summary>
    /// <param name="id">Id объявления на обновление.</param>
    /// <param name="bulletinDto">Формат данных обновления данных объявления.</param>
    /// <returns>Базовый формат данных объявления.</returns>
    public Task<BulletinMainDto?> UpdateAsync(Guid id, BulletinMainUpdateDto bulletinDto);

    /// <summary>
    /// Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Id объявления на удаление.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    public Task SaveChangesAsync();
}
