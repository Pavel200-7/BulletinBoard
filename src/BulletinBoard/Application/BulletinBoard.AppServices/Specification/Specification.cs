using BulletinBoard.AppServices.Specification.LogicalOperations;
using System.Linq.Expressions;


namespace BulletinBoard.AppServices.Specification;

/// <summary>
/// Абстрактный базовый класс для реализации спецификаций.
/// Спецификация представляет собой условие, которое можно применять к сущностям типа T
/// и использовать для отбора сущьностей в SQL запросах.
/// </summary>
public abstract class Specification<T>
{
    /// <summary>
    /// Абстрактный метод, который возвращает выражение LINQ,
    /// представляющее условие спецификации.
    /// </summary>
    public abstract Expression<Func<T, bool>> ToExpression();

    /// <summary>
    /// Объединяет текущую спецификацию с другой через логическую И (AND).
    /// Возвращает новую спецификацию, которая представляет собой комбинацию.
    /// </summary>
    /// <param name="specification">Другая спецификация для объединения.</param>
    /// <returns>Новая спецификация, объединяющая обе условия.</returns>
    public Specification<T> And(Specification<T> specification)
    {
        return new AndSpecification<T>(this, specification);
    }

    /// <summary>
    /// Объединяет текущую спецификацию с другой через логическую ИЛИ (OR).
    /// Возвращает новую спецификацию.
    /// </summary>
    /// <param name="specification">Другая спецификация для объединения.</param>
    /// <returns>Новая спецификация, объединяющая оба условия.</returns>
    public Specification<T> Or(Specification<T> specification)
    {
        return new OrSpecification<T>(this, specification);
    }

    /// <summary>
    /// Создает отрицание (NOT) указанной спецификации.
    /// </summary>
    /// <param name="specification">Спецификация, которую нужно инвертировать.</param>
    /// <returns>Новая спецификация, представляющая отрицание.</returns>
    public Specification<T> Not(Specification<T> specification)
    {
        return new NotSpecification<T>(this);
    }

}
