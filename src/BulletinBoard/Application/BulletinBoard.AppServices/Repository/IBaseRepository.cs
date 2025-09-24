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
    : ICRUDRepository<TDto, TCreateDto, TUpdateDto>
    where TEntity : EntityBase
    where TDto : class
    where TCreateDto : class
    where TUpdateDto : class
    
{
    /// <summary>
    /// Получить список сущностей по фильтру.
    /// </summary>
    /// <param name="specification">Расширенная спецификация для фильтрации.</param>
    /// <returns>Коллекция данных базового формата данных сущности.</returns>
    public Task<IReadOnlyCollection<TDto>> FindAsync(ExtendedSpecification<TEntity> specification);
}
