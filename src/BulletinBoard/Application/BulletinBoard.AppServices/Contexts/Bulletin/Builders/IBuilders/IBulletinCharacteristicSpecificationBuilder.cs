using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;

/// <summary>
/// Строитель (builder) для создания расширенных спецификаций 
/// для отбора сущностей BulletinCharacteristic в методах соответствующего репозитория.
/// </summary>
public interface IBulletinCharacteristicSpecificationBuilder : ISpecificationBuilder<BulletinCharacteristic>
{
    /// <summary>
    /// Добавить отбор по полному совпадению имени.
    /// </summary>
    /// <param name="name">Имя.</param>
    /// <returns>>Ссылка на builder (самого себя).</returns>
    IBulletinCharacteristicSpecificationBuilder WhereName(string name);

    /// <summary>
    /// Добавить отбор по частичному совпадению имени.
    /// </summary>
    /// <param name="name">Имя</param>
    /// <returns>>Ссылка на builder (самого себя).</returns>
    IBulletinCharacteristicSpecificationBuilder WhereNameContains(string name);

    /// <summary>
    /// Добавить отбор по categoryId.
    /// </summary>
    /// <param name="categoryId">Id категории</param>
    /// <returns>>Ссылка на builder (самого себя).</returns>
    IBulletinCharacteristicSpecificationBuilder WhereCategoryId(Guid categoryId);

    /// <summary>
    /// Добавить сортировку по имени.
    /// </summary>
    /// <param name="ascending">Направление сортировки (true - по возрастанию, false - по убыванию).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    public IBulletinCharacteristicSpecificationBuilder OrderByName(bool ascending = true);
}