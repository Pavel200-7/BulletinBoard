using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;

/// <summary>
/// Класс (builder) для создания расширенных спецификаций 
/// (которые содержат не только фильтрацию, но и сортировку по одному полю и пагинацию)
/// для для отбора сущностей BulletinCategory в методах соответствующего репозитория.
/// </summary>
public interface IBulletinCategorySpecificationBuilder
{
    /// <summary>
    /// Добавить отбор по полю ParentCategoryId.
    /// </summary>
    /// <param name="parentId">Id родительской категории.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinCategorySpecificationBuilder WhereParentId(Guid? parentId);

    /// <summary>
    /// Добавить отбор по полю CategoryName.
    /// </summary>
    /// <param name="categoryName">Имя категории.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinCategorySpecificationBuilder WhereCategoryName(string categoryName);

    /// <summary>
    /// Добавить отбор полей содержащих во фрагменте CategoryName следующую строку.
    /// </summary>
    /// <param name="categoryName">Фрагмент имени.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinCategorySpecificationBuilder WhereCategoryNameContains(string categoryName);

    /// <summary>
    /// Добавить отбор по полю IsLeafy.
    /// </summary>
    /// <param name="isLeafy"> Является ли листовой (типиковой) или может быть родительской (true, false).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinCategorySpecificationBuilder WhereIsLeafy(bool isLeafy);

    /// <summary>
    /// Добавить сортироваку по полю CategoryName.
    /// </summary>
    /// <param name="ascending">Добавлять ли сотрировку (true, false).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinCategorySpecificationBuilder OrderByCategoryName(bool ascending = true);

    /// <summary>
    /// Добавить сортироваку по полю IsLeafy.
    /// </summary>
    /// <param name="ascending">Добавлять ли сотрировку (true, false).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinCategorySpecificationBuilder OrderByIsLeafy(bool ascending = true);

    /// <summary>
    /// Добавить Пагинацию.
    /// </summary>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinCategorySpecificationBuilder Paginate(int pageNumber, int pageSize);

    /// <summary>
    /// Создать расширенную спецификацию, преобразуемую в sql запрос в репозитории.
    /// </summary>
    /// <returns>Готовая спецификация.</returns>
    public ExtendedSpecification<BulletinCategory> Build();
}