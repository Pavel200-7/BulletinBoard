using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;

/// <summary>
/// Класс (builder) для создания расширенных спецификаций 
/// (которые содержат не только фильтрацию, но и сортировку по одному полю и пагинацию)
/// для для отбора сущностей BulletinImage в методах соответствующего репозитория.
/// </summary>
public interface IBulletinImageSpecificationBuilder
{
    /// <summary>
    /// Добавить отбор по полю BelletinId.
    /// </summary>
    /// <param name="belletinId">Id объявления категории.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinImageSpecificationBuilder WhereBelletinId(Guid? belletinId);

    /// <summary>
    /// Добавить отбор по полю IsMain.
    /// </summary>
    /// <param name="isMain">Является ли титульным.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinImageSpecificationBuilder WhereIsMain(bool isMain);

    /// <summary>
    /// Добавить отбор по полю Name.
    /// </summary>
    /// <param name="imageName">Имя изображения.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinImageSpecificationBuilder WhereName(string imageName);

    /// <summary>
    /// Добавить отбор полей содержащих во фрагменте Name следующую строку.
    /// </summary>
    /// <param name="imageName">Фрагмент имени.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinImageSpecificationBuilder WhereNameContains(string imageName);

    /// <summary>
    /// Добавить отбор по полю сreatedAt.
    /// </summary>
    /// <param name="сreatedAt">Время создания</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinImageSpecificationBuilder WhereCreatedAt(DateTime сreatedAt);

    /// <summary>
    /// Добавить отбор по полю сreatedAt (до).
    /// В результат попадут изображения созданные до введенного значения.
    /// </summary>
    /// <param name="timePoint">Временная точка.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinImageSpecificationBuilder WhereCreatedAtBefore(DateTime timePoint);

    /// <summary>
    /// Добавить отбор по полю сreatedAt (после).
    /// В результат попадут изображения созданные после введенного значения.
    /// </summary>
    /// <param name="timePoint">Временная точка.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinImageSpecificationBuilder WhereCreatedAtAfter(DateTime timePoint);

    /// <summary>
    /// Добавить сортироваку по полю Name.
    /// </summary>
    /// <param name="ascending">Добавлять ли сотрировку (true, false).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinImageSpecificationBuilder OrderByName(bool ascending = true);

    /// <summary>
    /// Добавить сортироваку по полю IsMain.
    /// </summary>
    /// <param name="ascending">Добавлять ли сотрировку (true, false).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinImageSpecificationBuilder OrderByIsMain(bool ascending = true);

    /// <summary>
    /// Добавить сортироваку по полю CreatedAt.
    /// </summary>
    /// <param name="ascending">Добавлять ли сотрировку (true, false).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinImageSpecificationBuilder OrderByCreatedAt(bool ascending = true);

    /// <summary>
    /// Создать расширенную спецификацию на основе добавленных условий.
    /// </summary>
    /// <returns>Готовая спецификация для использования в репозитории.</returns>
    public ExtendedSpecification<BulletinImage> Build();
}
