using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicName;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinsCharacteristicValue
/// </summary>
public interface IBulletinCharacteristicValueRepository
{
    /// <summary>
    /// Получить возможне значение характеристики по идентификатору.
    /// </summary>
    /// <param name="id">Id возможного значения характеристики.</param>
    /// <returns>Базовый формат данных возможного значения характеристики.</returns>
    public Task<BulletinCharacteristicValueDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить список данных возможного значения характеристики по фильтру.
    /// </summary>
    /// <param name="specification">Расширенная спецификация для фильтрации.</param>
    /// <returns>Коллекция данных базового формата данных возможного значения характеристики.</returns>
    public Task<IReadOnlyCollection<BulletinCharacteristicValueDto>> FindAsync(ExtendedSpecification<BulletinCharacteristicValue> specification);

    /// <summary>
    /// Создать новые данные возможного значений характеристики.
    /// </summary>
    /// <param name="characteristicValueDto">Формат данных создания возможного значений характеристики.</param>
    /// <returns>Базовый формат данных возможного значения характеристики.</returns>
    public Task<BulletinCharacteristicValueDto> CreateAsync(BulletinCharacteristicValueCreateDto characteristicValueDto);

    /// <summary>
    /// Обновить данные возможного значений характеристики.
    /// </summary>
    /// <param name="id">Id данных возможного значений характеристики для обновления.</param>
    /// <param name="characteristicValueDto">Формат данных обновления возможного значения характеристики.</param>
    /// <returns>Базовый формат данных возможного значения характеристики.</returns>
    public Task<BulletinCharacteristicValueDto?> UpdateAsync(Guid id, BulletinCharacteristicValueUpdateDto characteristicValueDto);

    /// <summary>
    /// Удалить данные возможного значения характеристики по идентификатору.
    /// </summary>
    /// <param name="id">Id данных возможного значения характеристики.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);
}
