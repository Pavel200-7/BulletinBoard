using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис для работы с возможным значение характеристики объявления.
/// </summary>
public interface IBulletinCharacteristicValueService
{
    /// <summary>
    /// Получить возможное значение характеристики по идентификатору.
    /// </summary>
    /// <param name="id">Id значения характеристики.</param>
    /// <returns>Базовый формат данных значения характеристики.</returns>
    public Task<BulletinCharacteristicValueDto> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить список возможных значений характеристики по фильтру.
    /// </summary>
    /// <param name="сharacteristicValueFilterDto">Формат данных для фильтрации значений характеристики.</param>
    /// <returns>Коллекция базового формата данных значений характеристики.</returns>
    public Task<IReadOnlyCollection<BulletinCharacteristicValueDto>> GetAsync(BulletinCharacteristicValueFilterDto сharacteristicValueFilterDto);

    /// <summary>
    /// Создать новое возможное значение характеристики.
    /// </summary>
    /// <param name="сharacteristicValueDto">Формат данных создания значения характеристики.</param>
    /// <returns>Базовый формат данных значения характеристики.</returns>
    public Task<BulletinCharacteristicValueDto> CreateAsync(BulletinCharacteristicValueCreateDto сharacteristicValueDto);

    /// <summary>
    /// Обновить существующую возможное значение характеристики.
    /// </summary>
    /// <param name="id">Id категории для обновления.</param>
    /// <param name="сharacteristicValueDto">Данные для обновления значения характеристики.</param>
    /// <returns>Базовый формат данных значения характеристики.</returns>
    public Task<BulletinCharacteristicValueDto> UpdateAsync(Guid id, BulletinCharacteristicValueUpdateDto сharacteristicValueDto);

    /// <summary>
    /// Удалить значение характеристики по идентификатору.
    /// </summary>
    /// <param name="id">Id значения характеристики.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Получить список значений конкретной характеристики по ее id.
    /// </summary>
    /// <param name="characteristicId">id характеристики</param>
    /// <returns>Коллекция базового формата данных значений характеристики.</returns>
    public Task<IReadOnlyCollection<BulletinCharacteristicValueDto>> GetByCharacteristicAsync(Guid characteristicId);
}
