using BulletinBoard.AppServices.Specification;
using BulletinBoard.AppServices.Specification.LogicalOperations;
using BulletinBoard.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;

/// <summary>
/// Базовык класс для всех строителей спецификации с пагинацей курсором.
/// </summary>
/// <typeparam name="TEntity">Сущность.</typeparam>
public class CursorPaginationSpecificationBuilderBase<TEntity> : ICursorPaginationSpecificationBuilder<TEntity> where TEntity : EntityBase
{
    /// <summary>
    /// Расширенная спецификация.
    /// </summary>
    protected CompositeExtendedSpecification<TEntity> _specification;


    /// <summary>
    /// Конструктор, создающий основу спецификации.
    /// </summary>
    public CursorPaginationSpecificationBuilderBase()
    {
        _specification = new CompositeExtendedSpecification<TEntity>();
    }

    /// <summary>
    /// Установить спецификацию.
    /// </summary>
    public ICursorPaginationSpecificationBuilder<TEntity> SetSpecification(ExtendedSpecification<TEntity> specification)
    {
        if (specification is CompositeExtendedSpecification<TEntity> compositeSpec)
        {
            _specification = compositeSpec;
        }
        return this;
    }

    /// <summary>
    /// Выставить количество элементов выборки.
    /// </summary>
    /// <param name="items">Количество элементов на странице.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public ICursorPaginationSpecificationBuilder<TEntity> Take(int items)
    {
        _specification.Take = items;
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
