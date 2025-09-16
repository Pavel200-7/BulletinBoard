
using BulletinBoard.Contracts.Bulletin.BelletinMain;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис для работы с объявлениями
/// </summary>
public interface IBulletinMainService
{
    /// <summary>
    /// Создать объявление.
    /// </summary>
    /// <param name="bulletinDto">Формат данных создания объявления.</param>
    /// <returns>Базовый формат данных объявления</returns>
    public Task<BulletinMainDto> CreateAsync(BulletinMainCreateDto bulletinDto);

    /// <summary>
    /// Обновить объявление.
    /// </summary>
    /// <param name="id">Id объявления для обновления.</param>
    /// <param name="bulletinDto">Формат данных обновления данных объявления.</param>
    /// <returns>Базовый формат данных объявления.</returns>
    public Task<BulletinMainDto> UpdateAsync(Guid id, BulletinMainUpdateDto bulletinDto);

    /// <summary>
    /// Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Id объявления.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);
}
