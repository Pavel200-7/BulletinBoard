using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Base;
using System.Linq.Expressions;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;

/// <summary>
/// Базовык класс для всех строителей спецификации.
/// </summary>
/// <typeparam name="TEntity">Сущность.</typeparam>
public abstract class SpecificationBuilderBase<TEntity> : ISpecificationBuilder<TEntity> where TEntity : EntityBase
{
    /// <summary>
    /// Расширенная спецификация.
    /// </summary>
    protected CompositeExtendedSpecification<TEntity> _specification;

    /// <summary>
    /// Конструктор, создающий основу спецификации.
    /// </summary>
    public SpecificationBuilderBase()
    {
        _specification = new CompositeExtendedSpecification<TEntity>();
    }

    /// <summary>
    /// Добавить пагинацию.
    /// </summary>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Количество элементов.</param>
    /// <returns></returns>
    public ISpecificationBuilder<TEntity> Paginate(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        _specification.Skip = (pageNumber - 1) * pageSize;
        _specification.Take = pageSize;
        return this;
    }

    /// <summary>
    /// Создать расширенную спецификацию на основе добавленных условий.
    /// </summary>
    /// <returns>Готовая спецификация для использования в репозитории.</returns>
    public ExtendedSpecification<TEntity> Build()
    {
        return _specification;
    }
}
