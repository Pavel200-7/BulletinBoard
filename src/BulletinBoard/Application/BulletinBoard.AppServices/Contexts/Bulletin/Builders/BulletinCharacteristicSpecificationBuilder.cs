using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.Domain.Entities.Bulletin;
using System.Linq.Expressions;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders;

/// <inheritdoc/>
public class BulletinCharacteristicSpecificationBuilder : SpecificationBuilderBase<BulletinCharacteristic>, IBulletinCharacteristicSpecificationBuilder
{
    /// <inheritdoc/>
    public IBulletinCharacteristicSpecificationBuilder WhereName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            _specification.Add(c => c.Name == name);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinCharacteristicSpecificationBuilder WhereNameContains(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            _specification.Add(c => c.Name != null && c.Name.ToLower().Contains(name.ToLower()));
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinCharacteristicSpecificationBuilder WhereCategoryId(Guid categoryId)
    {
        if (categoryId != Guid.Empty)
        {
            _specification.Add(c => c.CategoryId == categoryId);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinCharacteristicSpecificationBuilder OrderByName(bool ascending = true)
    {
        Expression<Func<BulletinCharacteristic, object>>? orderByExpression = u => u.Name;
        _specification.AddOrderBy(orderByExpression, ascending);
        return this;
    }
}
