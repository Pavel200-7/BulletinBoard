using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.SpecificationBuilderBase;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders;

/// <inheritdoc/>
public class BulletinCharacteristicValueSpecificationBuilder : SpecificationBuilderBase<BulletinCharacteristicValue>, IBulletinCharacteristicValueSpecificationBuilder
{
    /// <inheritdoc/>
    public IBulletinCharacteristicValueSpecificationBuilder WhereCharacteristicId(Guid characteristicId)
    {
        if (characteristicId != Guid.Empty)
        {
            _specification.Add(c => c.CharacteristicId == characteristicId);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinCharacteristicValueSpecificationBuilder WhereValue(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _specification.Add(c => c.Value == value);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinCharacteristicValueSpecificationBuilder WhereValueContains(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _specification.Add(c => c.Value != null && c.Value.ToLower().Contains(value.ToLower()));
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinCharacteristicValueSpecificationBuilder OrderByValue(bool ascending = true)
    {
        _orderByExpression = c => c.Value;
        _orderByAscending = ascending;
        return this;
    }
}
