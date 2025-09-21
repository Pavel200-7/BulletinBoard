using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;

/// <summary>
/// Строитель (builder) для создания расширенных спецификаций 
/// для отбора сущностей BulletinMain в методах соответствующего репозитория.
/// Спецификации содержат фильтрацию, сортировку и пагинацию.
/// </summary>
public interface IBulletinMainSpecificationBuilder : ISpecificationBuilder<BulletinMain>
{
    /// <summary>
    /// Добавить отбор по полю UserId.
    /// </summary>
    /// <param name="userId">Id пользователя-создателя.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder WhereUserId(Guid? userId);

    /// <summary>
    /// Добавить отбор по полю Title.
    /// </summary>
    /// <param name="title">Заголовок объявления.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder WhereTitle(string title);

    /// <summary>
    /// Добавить отбор полей содержащих во фрагменте Title следующую строку.
    /// </summary>
    /// <param name="title">Фрагмент заголовка.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder WhereTitleContains(string title);

    /// <summary>
    /// Добавить отбор по полю Description.
    /// </summary>
    /// <param name="description">Описание объявления.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder WhereDescription(string description);

    /// <summary>
    /// Добавить отбор полей содержащих во фрагменте Description следующую строку.
    /// </summary>
    /// <param name="description">Фрагмент описания.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder WhereDescriptionContains(string description);

    /// <summary>
    /// Добавить отбор по полю CategoryId.
    /// </summary>
    /// <param name="categoryId">Id категории.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder WhereCategoryId(Guid? categoryId);

    /// <summary>
    /// Добавить отбор по диапазону цен.
    /// </summary>
    /// <param name="minPrice">Минимальная цена.</param>
    /// <param name="maxPrice">Максимальная цена.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder WherePriceRange(decimal? minPrice, decimal? maxPrice);

    /// <summary>
    /// Добавить отбор по дате создания (после указанной даты).
    /// </summary>
    /// <param name="createdAfter">Дата, после которой создано объявление.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder WhereCreatedAfter(DateTime? createdAfter);

    /// <summary>
    /// Добавить отбор по дате создания (до указанной даты).
    /// </summary>
    /// <param name="createdBefore">Дата, до которой создано объявление.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder WhereCreatedBefore(DateTime? createdBefore);

    /// <summary>
    /// Добавить отбор по статусу Hidden.
    /// </summary>
    /// <param name="hidden">Скрыто ли объявление.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder WhereHidden(bool hidden);

    /// <summary>
    /// Добавить отбор по статусу Closed.
    /// </summary>
    /// <param name="closed">Закрыто ли объявление.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder WhereClosed(bool closed);

    /// <summary>
    /// Добавить отбор по статусу Blocked.
    /// </summary>
    /// <param name="blocked">Заблокировано ли объявление.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder WhereBlocked(bool blocked);

    /// <summary>
    /// Добавить отбор только активных объявлений (не скрытых, не закрытых, не заблокированных).
    /// </summary>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder WhereActiveOnly();

    /// <summary>
    /// Добавить сортировку по дате создания.
    /// </summary>
    /// <param name="ascending">Направление сортировки (true - по возрастанию, false - по убыванию).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder OrderByCreatedAt(bool ascending = true);

    /// <summary>
    /// Добавить сортировку по цене.
    /// </summary>
    /// <param name="ascending">Направление сортировки (true - по возрастанию, false - по убыванию).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder OrderByPrice(bool ascending = true);

    /// <summary>
    /// Добавить сортировку по заголовку.
    /// </summary>
    /// <param name="ascending">Направление сортировки (true - по возрастанию, false - по убыванию).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinMainSpecificationBuilder OrderByTitle(bool ascending = true);
}