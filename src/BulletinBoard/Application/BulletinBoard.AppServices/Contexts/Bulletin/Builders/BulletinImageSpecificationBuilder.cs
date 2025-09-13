using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;
using System;
using System.Linq.Expressions;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders;

/// <inheritdoc/>
public class BulletinImageSpecificationBuilder : IBulletinImageSpecificationBuilder
{
    private readonly CompositeExtendedSpecification<BulletinImage> _specification;
    private Expression<Func<BulletinImage, object>>? _orderByExpression;
    private bool _orderByAscending = true;

    /// <summary>
    /// Инициализирует новый экземпляр строителя спецификаций.
    /// </summary>
    public BulletinImageSpecificationBuilder()
    {
        _specification = new CompositeExtendedSpecification<BulletinImage>();
    }

    /// <inheritdoc/>
    public IBulletinImageSpecificationBuilder WhereBelletinId(Guid? belletinId)
    {
        if (belletinId.HasValue)
        {
            _specification.Add(x => x.BelletinId == belletinId.Value);
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
    public IBulletinImageSpecificationBuilder WhereName(string imageName)
    {
        if (!string.IsNullOrEmpty(imageName))
        {
            _specification.Add(x => x.Name == imageName);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinImageSpecificationBuilder WhereNameContains(string imageName)
    {
        if (!string.IsNullOrEmpty(imageName))
        {
            _specification.Add(x => x.Name.Contains(imageName));
        }
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
    public IBulletinImageSpecificationBuilder OrderByName(bool ascending = true)
    {
        _orderByExpression = x => x.Name;
        _orderByAscending = ascending;
        return this;
    }

    /// <inheritdoc/>
    public IBulletinImageSpecificationBuilder OrderByIsMain(bool ascending = true)
    {
        _orderByExpression = x => x.IsMain;
        _orderByAscending = ascending;
        return this;
    }

    /// <inheritdoc/>
    public IBulletinImageSpecificationBuilder OrderByCreatedAt(bool ascending = true)
    {
        _orderByExpression = x => x.CreatedAt;
        _orderByAscending = ascending;
        return this;
    }

    /// <inheritdoc/>
    public ExtendedSpecification<BulletinImage> Build()
    {
        // Применяем сортировку
        if (_orderByExpression != null)
        {
            if (_orderByAscending)
            {
                _specification.OrderBy = _orderByExpression;
            }
            else
            {
                _specification.OrderByDescending = _orderByExpression;
            }
        }

        return _specification;
    }
}