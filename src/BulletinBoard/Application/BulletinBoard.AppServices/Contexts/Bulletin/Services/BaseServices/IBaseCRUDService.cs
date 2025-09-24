

using BulletinBoard.Contracts.Bulletin.BulletinCategory;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;

/// <summary>
/// Интерфейс сервисов с CRUD операциями
/// </summary>
public interface IBaseCRUDService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    where TEntityDto : class
    where TEntityCreateDto : class
    where TEntityUpdateDto : class
{
    /// <summary>
    /// Получить сущность по идентификатору.
    /// </summary>
    /// <param name="id">Id сущности.</param>
    /// <returns>Базовый формат данных сущности.</returns>
    public Task<TEntityDto> GetByIdAsync(Guid id);

    /// <summary>
    /// Создать новую сущность.
    /// </summary>
    /// <param name="createDto">Формат данных создания сущности.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных категории.</returns>
    public Task<TEntityDto> CreateAsync(TEntityCreateDto createDto, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить существующую сущность.
    /// </summary>
    /// <param name="id">Id сущности для обновления.</param>
    /// <param name="updateDto">Данные для обновления сущности.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных сущности.</returns>
    public Task<TEntityDto> UpdateAsync(Guid id, TEntityUpdateDto updateDto, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить сущность по идентификатору.
    /// </summary>
    /// <param name="id">Id сущности.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
