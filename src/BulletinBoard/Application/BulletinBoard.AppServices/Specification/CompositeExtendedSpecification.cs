using System.Linq.Expressions;


namespace BulletinBoard.AppServices.Specification;

/// <summary>
/// Реализация расширенной спецификации, позволяющая 
/// комбинировать несколько условий с помощью логической 
/// операции AND.
/// Позволяет динамически добавлять критерии фильтрации, 
/// объединяя их в одно итоговое выражение.
/// </summary>
public class CompositeExtendedSpecification<T> : ExtendedSpecification<T>
{
    private Expression<Func<T, bool>> _expression = x => true;

    /// <summary>
    /// Добавляет новый критерий (условие) к текущему объединению с помощью логического И (AND).
    /// </summary>
    /// <param name="criterion">Выражение, которое нужно добавить к текущему условию.</param>
    public void Add(Expression<Func<T, bool>> criterion)
    {
        var parameter = Expression.Parameter(typeof(T));
        var body = Expression.AndAlso(
            Expression.Invoke(_expression, parameter),
            Expression.Invoke(criterion, parameter)
        );
        _expression = Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>
    /// Возвращает совокупное выражение условий, представляющее все добавленные критерии.
    /// </summary>
    /// <returns>Объединённое выражение фильтрации.</returns>
    public override Expression<Func<T, bool>> ToExpression() => _expression;
}
