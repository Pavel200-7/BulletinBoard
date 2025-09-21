using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.AppServices.Specification.LogicalOperations;
using BulletinBoard.Domain.Entities.Bulletin;
using System.Linq.Expressions;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders;

/// <inheritdoc/>
public class BulletinCategorySpecificationBuilder : SpecificationBuilderBase<BulletinCategory>, IBulletinCategorySpecificationBuilder
{
    /// <inheritdoc/>
    public IBulletinCategorySpecificationBuilder WhereParentId(Guid? parentId)
    {
        if (parentId != null)
        {
            _specification.Add(c => c.ParentCategoryId == parentId.Value);
        }
        else
        {
            _specification.Add(c => c.ParentCategoryId == null);
        }

        return this;
    }

    /// <inheritdoc/>
    public IBulletinCategorySpecificationBuilder WhereCategoryName(string categoryName)
    {

        if (!string.IsNullOrEmpty(categoryName))
        {
            _specification.Add(c => c.CategoryName == categoryName);
        }

        return this;
    }

    /// <inheritdoc/>
    public IBulletinCategorySpecificationBuilder WhereCategoryNameContains(string categoryName)
    {

        if (!string.IsNullOrEmpty(categoryName))
        {
            _specification.Add(c => c.CategoryName.ToLower().Contains(categoryName.ToLower()));
        }

        return this;
    }

    /// <inheritdoc/>
    public IBulletinCategorySpecificationBuilder WhereIsLeafy(bool isLeafy = true)
    {
        _specification.Add(c => c.IsLeafy == isLeafy);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinCategorySpecificationBuilder OrderByCategoryName(bool ascending = true)
    {
        _orderByExpression = x => x.CategoryName;
        _orderByAscending = ascending;
        return this;
    }

    /// <inheritdoc/>
    public IBulletinCategorySpecificationBuilder OrderByIsLeafy(bool ascending = true)
    {
        _orderByExpression = x => x.IsLeafy;
        _orderByAscending = ascending;
        return this;
    }
}
