using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;

/// <summary>
/// Строитель (builder) для создания расширенных спецификаций 
/// для отбора сущностей BulletinCharacteristicValue.
/// </summary>
public interface IBulletinCharacteristicValueSpecificationBuilder : ISpecificationBuilder<BulletinCharacteristicValue>
{
    /// <summary>
    /// Добавить отбор по Id характеристики.
    /// </summary>
    /// <param name="characteristicId">Id характеристики</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinCharacteristicValueSpecificationBuilder WhereCharacteristicId(Guid characteristicId);

    /// <summary>
    /// Добавить отбор по значению.
    /// </summary>
    /// <param name="value">Значение характеристики</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinCharacteristicValueSpecificationBuilder WhereValue(string value);

    /// <summary>
    /// Добавить отбор по частичному совпадению значения.
    /// </summary>
    /// <param name="value">Частичное значение</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinCharacteristicValueSpecificationBuilder WhereValueContains(string value);

    /// <summary>
    /// Сортировка по значению.
    /// </summary>
    /// <param name="ascending">Направление сортировки (true - по возрастанию).</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinCharacteristicValueSpecificationBuilder OrderByValue(bool ascending = true);
}
