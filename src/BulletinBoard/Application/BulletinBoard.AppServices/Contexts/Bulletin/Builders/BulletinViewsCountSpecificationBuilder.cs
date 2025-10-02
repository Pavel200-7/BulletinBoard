using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.Domain.Entities.Bulletin;
using System.Linq.Expressions;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders;

/// <inheritdoc/>
public class BulletinViewsCountSpecificationBuilder : SpecificationBuilderBase<BulletinViewsCount>, IBulletinViewsCountSpecificationBuilder
{
    /// <inheritdoc/>
    public IBulletinViewsCountSpecificationBuilder WhereViewsCount(int minViews)
    {
        _specification.Add(r => r.ViewsCount >= minViews);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinViewsCountSpecificationBuilder WhereViewsCountGreaterThan(int minViews)
    {
        _specification.Add(r => r.ViewsCount > minViews);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinViewsCountSpecificationBuilder WhereViewsCountLessThan(int maxViews)
    {
        _specification.Add(r => r.ViewsCount < maxViews);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinViewsCountSpecificationBuilder OrderByViewsCount(bool ascending = true)
    {
        Expression<Func<BulletinViewsCount, object>>? orderByExpression = r => r.ViewsCount;
        _specification.AddOrderBy(orderByExpression, ascending);
        return this;
    }
}
