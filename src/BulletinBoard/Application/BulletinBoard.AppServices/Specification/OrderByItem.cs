using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Specification;

/// <summary>
/// Элемент списка сортировок.
/// </summary>
public class OrderByItem<T>
{
    /// <summary>
    /// Лямбла выражение, указывающие на поле сортировки.
    /// </summary>
    public Expression<Func<T, object>> OrderByExpression {  get; set; }

    /// <summary>
    /// Порядок сортировки.
    /// </summary>
    public bool OrderByAscending {  get; set; }
}
