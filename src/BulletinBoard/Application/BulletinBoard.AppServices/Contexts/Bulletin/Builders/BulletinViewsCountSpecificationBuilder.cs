using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.Domain.Entities.Bulletin;


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
        _orderByExpression = r => r.ViewsCount;
        _orderByAscending = ascending;
        return this;
    }
}
