using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис для работы с характеристиками объявлений.
/// </summary>
public interface IBulletinCharacteristicService
{
    /// <summary>
    /// Получить характеристику по идентификатору.
    /// </summary>
    /// <param name="id">Id характеристики.</param>
    /// <returns>Базовый формат данных характеристики.</returns>
    public Task<BulletinCharacteristicDto> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить список характеристики по фильтру.
    /// </summary>
    /// <param name="сharacteristicDto">Формат данных для фильтрации хакактеристик.</param>
    /// <returns>Коллекция базовый формат данных характеристики.</returns>
    public Task<IReadOnlyCollection<BulletinCharacteristicDto>> GetAsync(BulletinCharacteristicFilterDto сharacteristicDto);

    /// <summary>
    /// Создать новую характеристику.
    /// </summary>
    /// <param name="сharacteristicDto">Формат данных создания характеристики.</param>
    /// <returns>Базовый формат данных характеристики.</returns>
    public Task<BulletinCharacteristicDto> CreateAsync(BulletinCharacteristicCreateDto сharacteristicDto);

    /// <summary>
    /// Обновить существующую характеристику.
    /// </summary>
    /// <param name="id">Id характеристики для обновления.</param>
    /// <param name="сharacteristicDto">Данные для обновления характеристики.</param>
    /// <returns>Базовый формат данных характеристики.</returns>
    public Task<BulletinCharacteristicDto> UpdateAsync(Guid id, BulletinCharacteristicUpdateDto сharacteristicDto);

    /// <summary>
    /// Удалить характеристику по идентификатору.
    /// </summary>
    /// <param name="id">Id характеристики.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);
}
