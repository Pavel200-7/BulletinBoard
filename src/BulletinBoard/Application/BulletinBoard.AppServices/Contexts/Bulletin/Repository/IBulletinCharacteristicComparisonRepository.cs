using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinsCharacteristic
/// </summary>
public interface IBulletinCharacteristicComparisonRepository
{
    /// <summary>
    /// Получить данные сопоставления характеристики с объявлением по идентификатору. 
    /// </summary>
    /// <param name="id">Id сопоставления характеристики с объявлением</param>
    /// <returns>Базовый формат данных сопоставления характеристики с объявлением.</returns>
    public Task<BulletinCharacteristicComparisonDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить список данных сопоставления характеристики с объявлением по фильтру.
    /// </summary>
    /// <param name="specification">Расширенная спецификация для фильтрации.</param>
    /// <returns>Коллекция данных базового формата данных сопоставления характеристики с объявлением.</returns>
    public Task<IReadOnlyCollection<BulletinCharacteristicComparisonDto>> FindAsync(ExtendedSpecification<BulletinCharacteristicComparison> specification);

    /// <summary>
    /// Создать новое сопоставление характеристики с объявлением.
    /// </summary>
    /// <param name="characteristicComparisonDto">Формат данных создания данных сопоставления характеристики с объявлением</param>
    /// <returns>Базовый формат данных сопоставления характеристики с объявлением.</returns>
    public Task<BulletinCharacteristicComparisonDto> CreateAsync(BulletinCharacteristicComparisonCreateDto characteristicComparisonDto);

    /// <summary>
    /// Обновить существующую сопоставление характеристики с объявлением.
    /// </summary>
    /// <param name="id">Id сопоставления характеристики с объявлением для обновления.</param>
    /// <param name="characteristicComparisonDto">Формат данных обновления данных сопоставления характеристики с объявлением</param>
    /// <returns>Базовый формат данных сопоставления характеристики с объявлением.</returns>
    public Task<BulletinCharacteristicComparisonDto?> UpdateAsync(Guid id, BulletinCharacteristicComparisonUpdateDto characteristicComparisonDto);

    /// <summary>
    /// Удалить сопоставление характеристики с объявлением по идентификатору.
    /// </summary>
    /// <param name="id">Id сопоставления характеристики с объявлением для удаления.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    public Task SaveChangesAsync();
}
