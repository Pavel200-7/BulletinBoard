using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinCategory
/// </summary>
public interface IBulletinCategoryRepository
{
    /// <summary>
    /// Получить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Id категории.</param>
    /// <returns>Базовый формат данных категории.</returns>
    public Task<BulletinCategoryDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить список категорий по фильтру.
    /// </summary>
    /// <param name="specification">Расширенная спецификация для фильтрации.</param>
    /// <returns>Коллекция данных базового формата данных категории.</returns>
    public Task<IReadOnlyCollection<BulletinCategoryDto>> FindAsync(ExtendedSpecification<BulletinCategory> specification);

    /// <summary>
    /// Создать новую категорию.
    /// </summary>
    /// <param name="categoryDto">Формат данных создания категории.</param>
    /// <returns>Базовый формат данных категории.</returns>
    public Task<BulletinCategoryDto> CreateAsync(BulletinCategoryCreateDto categoryDto);

    /// <summary>
    /// Обновить существующую категорию.
    /// </summary>
    /// <param name="id">Id категории для обновления.</param>
    /// <param name="categoryDto">Данные для обновления категории.</param>
    /// <returns>Базовый формат данных категории.</returns>
    public Task<BulletinCategoryDto?> UpdateAsync(Guid id, BulletinCategoryUpdateDto categoryDto);

    /// <summary>
    /// Удалить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Id категории.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);
}
