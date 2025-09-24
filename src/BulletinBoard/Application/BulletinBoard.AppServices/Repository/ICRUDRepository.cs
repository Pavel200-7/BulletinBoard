namespace BulletinBoard.AppServices.Repository;

/// <summary>
/// Репозиторий с CRUD операциями.
/// </summary>
public interface ICRUDRepository
    <
    TDto,
    TCreateDto,
    TUpdateDto
    >
    where TDto : class
    where TCreateDto : class
    where TUpdateDto : class
{
    /// <summary>
    /// Получить сущность по идентификатору.
    /// </summary>
    /// <param name="id">Id сущности.</param>
    /// <returns>Базовый формат данных сущности или null если его нет.</returns>
    public Task<TDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Создать новую сущность.
    /// </summary>
    /// <param name="categoryDto">Формат данных создания сущности.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных сущности.</returns>
    public Task<TDto> CreateAsync(TCreateDto categoryDto, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить существующую сущность.
    /// </summary>
    /// <param name="id">Id сущности для обновления.</param>
    /// <param name="categoryDto">Данные для обновления сущности.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных сущности или null если его нет.</returns>
    public Task<TDto?> UpdateAsync(Guid id, TUpdateDto categoryDto, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить сущность по идентификатору.
    /// </summary>
    /// <param name="id">Id сущности.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
