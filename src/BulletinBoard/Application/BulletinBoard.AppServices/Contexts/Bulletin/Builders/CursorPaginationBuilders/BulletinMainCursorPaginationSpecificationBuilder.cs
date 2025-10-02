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

        //if (lastId.HasValue && !string.IsNullOrEmpty(lastTitle))
        //{
        //    if (ascending)
        //    {
        //        // Для ASC: ищем записи, которые идут ПОСЛЕ курсора
        //        _specification.Add(b => b.Title > lastTitle || (b.Title == lastTitle && b.Id > lastId));
        //    }
        //    else
        //    {
        //        // Для DESC: ищем записи, которые идут ДО курсора
        //        _specification.Add(b => b.Title < lastTitle || (b.Title == lastTitle && b.Id < lastId));
        //    }
        //}

        //// Важно: порядок сортировки должен соответствовать условию курсора
        //Expression<Func<BulletinMain, object>> titleOrderByExpression = b => b.Title;
        //_specification.AddOrderBy(titleOrderByExpression, ascending);

        //Expression<Func<BulletinMain, object>> idOrderByExpression = b => b.Id;
        //_specification.AddOrderBy(idOrderByExpression, ascending);
        return this;
    }
}
