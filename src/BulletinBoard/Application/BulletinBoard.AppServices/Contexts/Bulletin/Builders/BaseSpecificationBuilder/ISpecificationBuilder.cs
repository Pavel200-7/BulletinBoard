using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Base;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;

/// <summary>
/// Интерфейс с базовыми операциями строителя спецификации.
/// </summary>
public interface ISpecificationBuilder<TEntity> where TEntity : EntityBase
{
    /// <summary>
    /// Добавить пагинацию для результатов запроса.
    /// </summary>
    /// <param name="pageNumber">Номер страницы (начинается с 1).</param>
    /// <param name="pageSize">Количество элементов на странице.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public ISpecificationBuilder<TEntity> Paginate(int pageNumber, int pageSize);

    /// <summary>
    /// Создать расширенную спецификацию на основе добавленных условий.
    /// </summary>
    /// <returns>Готовая спецификация для использования в репозитории.</returns>
    public ExtendedSpecification<TEntity> Build();
}
