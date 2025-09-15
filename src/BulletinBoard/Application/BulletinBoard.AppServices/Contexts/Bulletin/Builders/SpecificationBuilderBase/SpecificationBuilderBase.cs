using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Specification;
using System.Linq.Expressions;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders.SpecificationBuilderBase;

/// <summary>
/// Базовык класс для всех строителей спецификации.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SpecificationBuilderBase<T>
{
    /// <summary>
    /// Расширенная спецификация.
    /// </summary>
    protected CompositeExtendedSpecification<T> _specification;

    /// <summary>
    /// Выражение сортировки.
    /// </summary>
    protected Expression<Func<T, object>>? _orderByExpression;

    /// <summary>
    /// Порядок сортировки (возрастание/убывание).
    /// </summary>
    protected bool _orderByAscending = true;

    /// <summary>
    /// Конструктор, создающий основу спецификации.
    /// </summary>
    public SpecificationBuilderBase()
    {
        _specification = new CompositeExtendedSpecification<T>();
    }

    /// <summary>
    /// Создать расширенную спецификацию на основе добавленных условий.
    /// </summary>
    /// <returns>Готовая спецификация для использования в репозитории.</returns>
    public ExtendedSpecification<T> Build()
    {
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
