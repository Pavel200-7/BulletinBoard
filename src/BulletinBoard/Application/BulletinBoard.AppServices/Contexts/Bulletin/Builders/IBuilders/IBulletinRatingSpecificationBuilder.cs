using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;

/// <summary>
/// Класс (builder) для создания расширенных спецификаций 
/// (которые содержат не только фильтрацию, но и сортировку по одному полю и пагинацию)
/// для для отбора сущностей BulletinCategoryRating в методах соответствующего репозитория.
/// </summary>
public interface IBulletinRatingSpecificationBuilder : ISpecificationBuilder<BulletinRating>
{
    /// <summary>
    /// Отбор по конкретному BulletinId.
    /// </summary>
    /// <param name="bulletinId">ID объявления.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinRatingSpecificationBuilder WhereBulletinId(Guid bulletinId);

    /// <summary>
    /// Отбор по рейтингу.
    /// </summary>
    /// <param name="maxRating">Максимальный рейтинг.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinRatingSpecificationBuilder WhereRating(decimal maxRating);

    /// <summary>
    /// Отбор по минимальному рейтингу.
    /// </summary>
    /// <param name="minRating">Минимальный рейтинг.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinRatingSpecificationBuilder WhereRatingGreaterThan(decimal minRating);

    /// <summary>
    /// Отбор по максимальному рейтингу.
    /// </summary>
    /// <param name="maxRating">Максимальный рейтинг.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinRatingSpecificationBuilder WhereRatingLessThan(decimal maxRating);

    /// <summary>
    /// Добавить сортировку по рейтингу.
    /// </summary>
    /// <param name="ascending">Направление сортировки (true - по возрастанию, false - по убыванию).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinRatingSpecificationBuilder OrderByRating(bool ascending = true);
}