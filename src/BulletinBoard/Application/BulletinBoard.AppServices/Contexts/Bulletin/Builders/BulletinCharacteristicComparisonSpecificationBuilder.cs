using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders;

/// <inheritdoc/>
public class BulletinCharacteristicComparisonSpecificationBuilder : SpecificationBuilderBase<BulletinCharacteristicComparison>, IBulletinCharacteristicComparisonSpecificationBuilder
{
    /// <inheritdoc/>
    public IBulletinCharacteristicComparisonSpecificationBuilder WhereBulletinId(Guid bulletinId)
    {
        _specification.Add(c => c.BulletinId == bulletinId);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinCharacteristicComparisonSpecificationBuilder WhereCharacteristicId(Guid characteristicId)
    {
        _specification.Add(c => c.CharacteristicId == characteristicId);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinCharacteristicComparisonSpecificationBuilder WhereCharacteristicValueId(Guid characteristicValueId)
    {
        _specification.Add(c => c.CharacteristicValueId == characteristicValueId);
        return this;
    }
}