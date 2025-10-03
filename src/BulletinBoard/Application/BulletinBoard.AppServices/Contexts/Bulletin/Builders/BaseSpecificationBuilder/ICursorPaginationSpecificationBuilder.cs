using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;

/// <summary>
/// Интерфейс с базовыми операциями строителя спецификации пагинации курсором.
/// </summary>
public interface ICursorPaginationSpecificationBuilder<TEntity> where TEntity : EntityBase
{
    /// <summary>
    /// Установить спецификацию.
    /// </summary>
    /// <param name="specification">Спецификация.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public ICursorPaginationSpecificationBuilder<TEntity> SetSpecification(ExtendedSpecification<TEntity> specification);

    /// <summary>
    /// Выставить количество элементов выборки.
    /// </summary>
    /// <param name="items">Количество элементов на странице.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public ICursorPaginationSpecificationBuilder<TEntity> Take(int items);

    /// <summary>
    /// Создать расширенную спецификацию на основе добавленных условий.
    /// </summary>
    /// <returns>Готовая спецификация для использования в репозитории.</returns>
    public ExtendedSpecification<TEntity> Build();
}
