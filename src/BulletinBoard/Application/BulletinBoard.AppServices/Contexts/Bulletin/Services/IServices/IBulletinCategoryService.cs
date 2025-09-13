using BulletinBoard.Contracts.Bulletin.BulletinCategory;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис для работы с категориями объявлений.
/// </summary>
public interface IBulletinCategoryService
{
    /// <summary>
    /// Получить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Id категории.</param>
    /// <returns>Базовый формат данных категории.</returns>
    public Task<BulletinCategoryDto> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить список категорий по фильтру.
    /// </summary>
    /// <param name="categoryDto">Формат данных для фильтрации категории по id родительской и (или) по названию.</param>
    /// <returns>Коллекция базовый формат данных категории.</returns>
    public Task<IReadOnlyCollection<BulletinCategoryDto>> GetAsync(BulletinCategoryFilterDto categoryDto);

    /// <summary>
    /// Получить категории в формате правильной иерархии.
    /// </summary>
    /// <returns>Формат данных для вывода всех категорий в их правильном иерархическом виде.</returns>
    public Task<BulletinCategoryReadAllDto> GetAllAsync();

    /// <summary>
    /// Получить карегорию в виде древовидной струкруры от самого корня.
    /// </summary>
    /// <param name="id">Id категории.</param>
    /// <returns> Формат данных для вывода одной карегории в виде древовидной струкруры от самого корня.</returns>
    public Task<BulletinCategoryReadSingleDto> GetSingleAsync(Guid id);

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
    public Task<BulletinCategoryDto> UpdateAsync(Guid id, BulletinCategoryUpdateDto categoryDto);

    /// <summary>
    /// Удалить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Id категории.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);
}
