using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;

/// <summary>
/// Строитель (builder) для создания расширенных спецификаций 
/// для отбора сущностей BulletinCharacteristicComparison в методах соответствующего репозитория.
/// Спецификации содержат фильтрацию, сортировку и пагинацию.
/// </summary>
public interface IBulletinCharacteristicComparisonSpecificationBuilder : ISpecificationBuilder<BulletinCharacteristicComparison>
{
    /// <summary>
    /// Добавить отбор по id объявления.
    /// </summary>
    /// <param name="bulletinId">id объявления.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinCharacteristicComparisonSpecificationBuilder WhereBulletinId(Guid bulletinId);

    /// <summary>
    /// Добавить отбор по id харакеристики.
    /// </summary>
    /// <param name="characteristicId">id характеристики.</param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinCharacteristicComparisonSpecificationBuilder WhereCharacteristicId(Guid characteristicId);

    /// <summary>
    /// Добавить отбор по id значения характеристики.
    /// </summary>
    /// <param name="characteristicValueId"></param>
    /// <returns>Ссылка на builder (самого себя).</returns>
    IBulletinCharacteristicComparisonSpecificationBuilder WhereCharacteristicValueId(Guid characteristicValueId);
}