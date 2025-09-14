using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinImages
/// </summary>
public interface IBulletinImageRepository
{
    /// <summary>
    /// Получить данные изображения по идентификатору.
    /// </summary>
    /// <param name="id">Id изображения.</param>
    /// <returns>Базовый формат данных изображения объявления.</returns>
    public Task<BulletinImageDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить список с данными изображения по фильтру.
    /// </summary>
    /// <param name="specification">Расширенная спецификация для фильтрации.</param>
    /// <returns>Коллекция данных базового формата данных изображения объявления.</returns>
    public Task<IReadOnlyCollection<BulletinImageDto>> FindAsync(ExtendedSpecification<BulletinImage> specification);

    /// <summary>
    /// Добавить новое изображения.
    /// </summary>
    /// <param name="imageDto">Формат данных добавления изображения объявления.</param>
    /// <returns>Базовый формат данных изображения объявления.</returns>
    public Task<BulletinImageDto> CreateAsync(BulletinImageCreateDto imageDto);

    /// <summary>
    /// Обновить существующее изображения.
    /// </summary>
    /// <param name="id">Id изображения для обновления.</param>
    /// <param name="imageDto">Формат данных обновления изображения объявления.</param>
    /// <returns>Базовый формат данных изображения объявления.</returns>
    public Task<BulletinImageDto?> UpdateAsync(Guid id, BulletinImageUpdateDto imageDto);

    /// <summary>
    /// Удалить изображение по идентификатору.
    /// </summary>
    /// <param name="id">Id изображения.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    public Task SaveChangesAsync();
}
