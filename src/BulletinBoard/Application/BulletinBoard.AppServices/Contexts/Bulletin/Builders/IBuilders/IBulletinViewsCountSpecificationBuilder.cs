using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.Domain.Entities.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;

/// <summary>
/// Строитель (builder) для создания расширенных спецификаций 
/// для отбора сущностей BulletinViewsCount в методах соответствующего репозитория.
/// Спецификации содержат фильтрацию, сортировку и пагинацию.
/// </summary>
public interface IBulletinViewsCountSpecificationBuilder : ISpecificationBuilder<BulletinViewsCount>
{
    /// <summary>
    /// Отбор по количеству просмотров.
    /// </summary>
    /// <param name="minViews">Минимальное число просмотров.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinViewsCountSpecificationBuilder WhereViewsCount(int minViews);

    /// <summary>
    /// Отбор по минимальному количеству просмотров.
    /// </summary>
    /// <param name="minViews">Минимальное число просмотров.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinViewsCountSpecificationBuilder WhereViewsCountGreaterThan(int minViews);

    /// <summary>
    /// Отбор по максимальному количеству просмотров.
    /// </summary>
    /// <param name="maxViews">Максимальное число просмотров.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinViewsCountSpecificationBuilder WhereViewsCountLessThan(int maxViews);

    /// <summary>
    /// Добавить сортировку по количеству просмотров.
    /// </summary>
    /// <param name="ascending">Направление сортировки (true - по возрастанию, false - по убыванию).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinViewsCountSpecificationBuilder OrderByViewsCount(bool ascending = true);
}
