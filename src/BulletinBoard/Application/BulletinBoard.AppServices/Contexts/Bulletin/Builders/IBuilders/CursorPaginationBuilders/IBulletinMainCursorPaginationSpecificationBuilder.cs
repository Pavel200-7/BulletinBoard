using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;

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

}