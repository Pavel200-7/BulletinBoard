using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;
using System;
using System.Linq.Expressions;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders;

/// <inheritdoc/>
public class BulletinImageSpecificationBuilder : SpecificationBuilderBase<BulletinImage>, IBulletinImageSpecificationBuilder
{
    /// <inheritdoc/>
    public IBulletinImageSpecificationBuilder WhereBelletinId(Guid? belletinId)
    {
        if (belletinId.HasValue)
        {
            _specification.Add(x => x.BulletinId == belletinId.Value);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinImageSpecificationBuilder WhereIsMain(bool isMain)
    {
        _specification.Add(x => x.IsMain == isMain);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinImageSpecificationBuilder WhereCreatedAt(DateTime createdAt)
    {
        _specification.Add(x => x.CreatedAt == createdAt);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinImageSpecificationBuilder WhereCreatedAtBefore(DateTime timePoint)
    {
        _specification.Add(x => x.CreatedAt <= timePoint);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinImageSpecificationBuilder WhereCreatedAtAfter(DateTime timePoint)
    {
        _specification.Add(x => x.CreatedAt >= timePoint);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinImageSpecificationBuilder OrderByIsMain(bool ascending = true)
    {
        Expression<Func<BulletinImage, object>>? orderByExpression = x => x.IsMain;
        _specification.AddOrderBy(orderByExpression, ascending);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinImageSpecificationBuilder OrderByCreatedAt(bool ascending = true)
    {
        Expression<Func<BulletinImage, object>>? orderByExpression = x => x.CreatedAt;
        _specification.AddOrderBy(orderByExpression, ascending);
        return this;
    }
}