using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Specification;

/// <summary>
/// Абстрактный класс, расширяющий базовую спецификацию,
/// позволяя добавлять параметры сортировки, пагинации 
/// и включения связанных сущностей.
/// Используется для построения более сложных запросов 
/// к данным, которые включают сортировку, пропуски, 
/// выборки и связанные данные.
/// </summary>
public abstract class ExtendedSpecification<T> : Specification<T>
{
    /// <summary>
    /// Определяет выражение для сортировки по возрастанию.
    /// </summary>
    public Expression<Func<T, object>>? OrderBy { get; set; }

    /// <summary>
    /// Определяет выражение для сортировки по убыванию.
    /// </summary>
    public Expression<Func<T, object>>? OrderByDescending { get; set; }

    /// <summary>
    /// Количество элементов, которые нужно пропустить (используется для пагинации).
    /// </summary>
    public int? Skip { get; set; }

    /// <summary>
    /// Количество элементов, которые нужно взять (используется для пагинации).
    /// </summary>
    public int? Take { get; set; }

    /// <summary>
    /// Коллекция выражений для включения связанных сущностей (например, для загрузки связанных данных через Include).
    /// </summary>
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
}
