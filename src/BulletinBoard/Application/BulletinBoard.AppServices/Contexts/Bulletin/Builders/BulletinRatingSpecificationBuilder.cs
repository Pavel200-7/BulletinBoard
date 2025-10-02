using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.AppServices.Specification.LogicalOperations;
using BulletinBoard.Domain.Entities.Bulletin;
using System;
using System.Linq.Expressions;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders;

/// <inheritdoc/>
public class BulletinRatingSpecificationBuilder : SpecificationBuilderBase<BulletinRating>, IBulletinRatingSpecificationBuilder
{
    /// <inheritdoc/>
    public IBulletinRatingSpecificationBuilder WhereBulletinId(Guid bulletinId)
    {
        _specification.Add(r => r.BulletinId == bulletinId);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinRatingSpecificationBuilder WhereRating(decimal maxRating)
    {
        _specification.Add(r => r.Rating <= maxRating);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinRatingSpecificationBuilder WhereRatingGreaterThan(decimal minRating)
    {
        _specification.Add(r => r.Rating > minRating);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinRatingSpecificationBuilder WhereRatingLessThan(decimal maxRating)
    {
        _specification.Add(r => r.Rating < maxRating);
        return this;
    }


    /// <inheritdoc/>
    public IBulletinRatingSpecificationBuilder OrderByRating(bool ascending = true)
    {
        Expression<Func<BulletinRating, object>>? orderByExpression = r => r.Rating;
        _specification.AddOrderBy(orderByExpression, ascending);
        return this;
    }
}