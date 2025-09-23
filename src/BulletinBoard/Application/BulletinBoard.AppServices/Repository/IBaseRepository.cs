using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Base;


namespace BulletinBoard.AppServices.Repository;

/// <summary>
/// Интерфейс, включающий в себя базовые операции с сущностью,
/// которые повторяются для всех сущностей.
/// </summary>
public interface IBaseRepository
    <
    TEntity,
    TDto,
    TCreateDto,
    TUpdateDto
    >
    where TEntity : EntityBase
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
    /// Получить список сущностей по фильтру.
    /// </summary>
    /// <param name="specification">Расширенная спецификация для фильтрации.</param>
    /// <returns>Коллекция данных базового формата данных сущности.</returns>
    public Task<IReadOnlyCollection<TDto>> FindAsync(ExtendedSpecification<TEntity> specification);

    /// <summary>
    /// Создать новую сущность.
    /// </summary>
    /// <param name="categoryDto">Формат данных создания сущности.</param>
    /// <returns>Базовый формат данных сущности.</returns>
    public Task<TDto> CreateAsync(TCreateDto categoryDto);

    /// <summary>
    /// Обновить существующую сущность.
    /// </summary>
    /// <param name="id">Id сущности для обновления.</param>
    /// <param name="categoryDto">Данные для обновления сущности.</param>
    /// <returns>Базовый формат данных сущности или null если его нет.</returns>
    public Task<TDto?> UpdateAsync(Guid id, TUpdateDto categoryDto);

    /// <summary>
    /// Удалить сущность по идентификатору.
    /// </summary>
    /// <param name="id">Id сущности.</param>
    /// <returns>Истина, если удаление прошло успешно; иначе ложь.</returns>
    public Task<bool> DeleteAsync(Guid id);
}
