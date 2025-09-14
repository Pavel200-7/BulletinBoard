using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicName;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinsCharacteristicName
/// </summary>
public interface IBulletinCharacteristicRepository
{
    /// <summary>
    /// Получить характеристику по идентификатору.
    /// </summary>
    /// <param name="id">Id характеристики.</param>
    /// <returns>Базовый формат данных характеристики.</returns>
    public Task<BulletinCharacteristicDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить список характеристики по фильтру.
    /// </summary>
    /// <param name="specification">Расширенная спецификация для фильтрации.</param>
    /// <returns>Коллекция данных базового формата данных характеристики.</returns>
    public Task<IReadOnlyCollection<BulletinCharacteristicDto>> FindAsync(ExtendedSpecification<BulletinCharacteristic> specification);

    /// <summary>
    /// Создать новую характеристику.
    /// </summary>
    /// <param name="characteristicDto">Формат данных создания характеристики.</param>
    /// <returns>Базовый формат данных характеристики.</returns>
    public Task<BulletinCharacteristicDto> CreateAsync(BulletinCharacteristicCreateDto characteristicDto);

    /// <summary>
    /// Обновить существующую характеристику.
    /// </summary>
    /// <param name="id">Id характеристики для обновления.</param>
    /// <param name="characteristicDto">Формат данных обновления характеристики.</param>
    /// <returns>Базовый формат данных характеристики.</returns>
    public Task<BulletinCharacteristicDto?> UpdateAsync(Guid id, BulletinCharacteristicUpdateDto characteristicDto);

    /// <summary>
    /// Удалить характеристику по идентификатору.
    /// </summary>
    /// <param name="id">Id характеристики.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    public Task SaveChangesAsync();
}
