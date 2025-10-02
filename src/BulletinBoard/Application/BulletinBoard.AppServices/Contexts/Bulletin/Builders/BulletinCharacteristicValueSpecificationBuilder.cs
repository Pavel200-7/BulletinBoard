using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.Domain.Entities.Bulletin;
using System.Linq.Expressions;


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
        Expression<Func<BulletinCharacteristicValue, object>>? orderByExpression = c => c.Value;
        _specification.AddOrderBy(orderByExpression, ascending);
        return this;
    }
}
