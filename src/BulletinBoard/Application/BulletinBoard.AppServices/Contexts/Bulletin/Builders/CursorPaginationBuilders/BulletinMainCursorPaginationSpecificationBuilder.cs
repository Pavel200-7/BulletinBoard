using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.BaseSpecificationBuilder;
using BulletinBoard.Domain.Entities.Bulletin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders.CursorPaginationBuilders;

/// <inheritdoc/>
public class BulletinMainCursorPaginationSpecificationBuilder : CursorPaginationSpecificationBuilderBase<BulletinMain>, IBulletinMainCursorPaginationSpecificationBuilder
{
    /// <inheritdoc/>
    public IBulletinMainCursorPaginationSpecificationBuilder PaginateByTitle(Guid? lastId, string? lastTitle, bool ascending)
    {

        if (lastId.HasValue && !string.IsNullOrEmpty(lastTitle))
        {
            if (ascending)
            {
                _specification.Add(b => b.Title.CompareTo(lastTitle) >= 0 || (b.Title == lastTitle && b.Id >= lastId));
            }
            else
            {
                _specification.Add(b => b.Title.CompareTo(lastTitle) <= 0 || (b.Title == lastTitle && b.Id <= lastId));
            }
        }


        Expression<Func<BulletinMain, object>> titleOrderByExpression = b => b.Title;
        _specification.AddOrderBy(titleOrderByExpression, ascending);

        Expression<Func<BulletinMain, object>> idOrderByExpression = b => b.Id;
        _specification.AddOrderBy(idOrderByExpression, ascending);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainCursorPaginationSpecificationBuilder PaginateByDate(Guid? lastId, string? lastTitle, bool ascending)
    {

        if (lastId.HasValue && !string.IsNullOrEmpty(lastTitle))
        {
            if (ascending)
            {
                _specification.Add(b => b.Title.CompareTo(lastTitle) >= 0 || (b.Title == lastTitle && b.Id >= lastId));
            }
            else
            {
                _specification.Add(b => b.Title.CompareTo(lastTitle) <= 0 || (b.Title == lastTitle && b.Id <= lastId));
            }
        }


        Expression<Func<BulletinMain, object>> titleOrderByExpression = b => b.Title;
        _specification.AddOrderBy(titleOrderByExpression, ascending);

        Expression<Func<BulletinMain, object>> idOrderByExpression = b => b.Id;
        _specification.AddOrderBy(idOrderByExpression, ascending);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainCursorPaginationSpecificationBuilder PaginateByDate(Guid? lastId, DateTime? lastDate, bool ascending)
    {

        if (lastId.HasValue && lastDate.HasValue)
        {
            if (ascending)
            {
                _specification.Add(b => b.CreatedAt >= lastDate || (b.CreatedAt == lastDate && b.Id >= lastId));
            }
            else
            {
                _specification.Add(b => b.CreatedAt <= lastDate || (b.CreatedAt == lastDate && b.Id <= lastId));
            }
        }


        Expression<Func<BulletinMain, object>> dateOrderByExpression = b => b.CreatedAt;
        _specification.AddOrderBy(dateOrderByExpression, ascending);

        Expression<Func<BulletinMain, object>> idOrderByExpression = b => b.Id;
        _specification.AddOrderBy(idOrderByExpression, ascending);
        return this;
    }

    /// <inheritdoc/>
    public IBulletinMainCursorPaginationSpecificationBuilder PaginateByPrice(Guid? lastId, decimal? lastPrice, bool ascending)
    {

        if (lastId.HasValue && lastPrice.HasValue)
        {
            if (ascending)
            {
                _specification.Add(b => b.Price >= lastPrice || (b.Price == lastPrice && b.Id >= lastId));
            }
            else
            {
                _specification.Add(b => b.Price <= lastPrice || (b.Price == lastPrice && b.Id <= lastId));
            }
        }


        Expression<Func<BulletinMain, object>> priceOrderByExpression = b => b.Price;
        _specification.AddOrderBy(priceOrderByExpression, ascending);

        Expression<Func<BulletinMain, object>> idOrderByExpression = b => b.Id;
        _specification.AddOrderBy(idOrderByExpression, ascending);
        return this;
    }
}
