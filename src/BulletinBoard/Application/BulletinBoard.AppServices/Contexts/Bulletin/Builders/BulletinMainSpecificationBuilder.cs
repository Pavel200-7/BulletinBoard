using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;
using System.Linq.Expressions;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders;

/// <inheritdoc/>
public class BulletinMainSpecificationBuilder : SpecificationBuilderBase<BulletinMain>, IBulletinMainSpecificationBuilder
{
    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder WhereUserId(Guid? userId)
    {
        if (userId.HasValue)
        {
            _specification.Add(b => b.UserId == userId.Value);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder WhereTitle(string title)
    {
        if (!string.IsNullOrEmpty(title))
        {
            _specification.Add(b => b.Title == title);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder WhereTitleContains(string title)
    {
        if (!string.IsNullOrEmpty(title))
        {
            _specification.Add(b => b.Title.ToLower().Contains(title.ToLower()));
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder WhereDescription(string description)
    {
        if (!string.IsNullOrEmpty(description))
        {
            _specification.Add(b => b.Description == description);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder WhereDescriptionContains(string description)
    {
        if (!string.IsNullOrEmpty(description))
        {
            _specification.Add(b => b.Description.ToLower().Contains(description.ToLower()));
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder WhereCategoryId(Guid? categoryId)
    {
        if (categoryId.HasValue)
        {
            _specification.Add(b => b.CategoryId == categoryId.Value);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder WherePriceRange(decimal? minPrice, decimal? maxPrice)
    {
        if (minPrice.HasValue && maxPrice.HasValue)
        {
            _specification.Add(b => b.Price >= minPrice.Value && b.Price <= maxPrice.Value);
        }
        else if (minPrice.HasValue)
        {
            _specification.Add(b => b.Price >= minPrice.Value);
        }
        else if (maxPrice.HasValue)
        {
            _specification.Add(b => b.Price <= maxPrice.Value);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder WhereCreatedAfter(DateTime? createdAfter)
    {
        if (createdAfter.HasValue)
        {
            _specification.Add(b => b.CreatedAt >= createdAfter.Value);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder WhereCreatedBefore(DateTime? createdBefore)
    {
        if (createdBefore.HasValue)
        {
            _specification.Add(b => b.CreatedAt <= createdBefore.Value);
        }
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder WhereHidden(bool hidden)
    {
        _specification.Add(b => b.Hidden == hidden);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder WhereClosed(bool closed)
    {
        _specification.Add(b => b.Closed == closed);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder WhereBlocked(bool blocked)
    {
        _specification.Add(b => b.Blocked == blocked);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder WhereActiveOnly()
    {
        _specification.Add(b => b.Hidden == false && b.Closed == false && b.Blocked == false);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder OrderByCreatedAt(bool ascending = true)
    {
        _orderByExpression = b => b.CreatedAt;
        _orderByAscending = ascending;
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder OrderByPrice(bool ascending = true)
    {
        _orderByExpression = b => b.Price;
        _orderByAscending = ascending;
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainSpecificationBuilder OrderByTitle(bool ascending = true)
    {
        _orderByExpression = b => b.Title;
        _orderByAscending = ascending;
        return this;
    }
}