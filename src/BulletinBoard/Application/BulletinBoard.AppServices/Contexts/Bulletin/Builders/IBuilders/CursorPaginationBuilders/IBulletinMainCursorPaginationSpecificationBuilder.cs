using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.AppServices.Specification.LogicalOperations;
using BulletinBoard.Domain.Entities.Bulletin;
using System.Linq.Expressions;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;

/// <summary>
/// Строитель (builder) для создания расширенных спецификаций 
/// для отбора сущностей BulletinMain в методах соответствующего репозитория.
/// Спецификации содержат фильтрацию, сортировку и пагинацию.
/// </summary>
public interface IBulletinMainCursorPaginationSpecificationBuilder : ICursorPaginationSpecificationBuilder<BulletinMain>
{
    /// <summary>
    /// Делает пагинацию с сортировкой по заголовку.
    /// </summary>
    /// <param name="lastId">Последний id объявления.</param>
    /// <param name="lastTitle">Последний заголовок объявления.</param>
    /// <param name="ascending">Порядок сортировки (true - ASC, false - DESC).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainCursorPaginationSpecificationBuilder PaginateByTitle(Guid? lastId, string? lastTitle, bool ascending);

    /// <summary>
    /// Делает пагинацию с сортировкой по дате.
    /// </summary>
    /// <param name="lastId">Последний id объявления.</param>
    /// <param name="lastDate">Последняя дата создания объявления.</param>
    /// <param name="ascending">Порядок сортировки (true - ASC, false - DESC).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainCursorPaginationSpecificationBuilder PaginateByDate(Guid? lastId, DateTime? lastDate, bool ascending);

    /// <summary>
    /// Делает пагинацию с сортировкой по цене.
    /// </summary>
    /// <param name="lastId">Последний id объявления.</param>
    /// <param name="lastPrice">Последний цена объявления.</param>
    /// <param name="ascending">Порядок сортировки (true - ASC, false - DESC).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainCursorPaginationSpecificationBuilder PaginateByPrice(Guid? lastId, decimal? lastPrice, bool ascending);
}