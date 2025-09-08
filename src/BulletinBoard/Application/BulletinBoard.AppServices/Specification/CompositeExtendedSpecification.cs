using System.Linq.Expressions;


namespace BulletinBoard.AppServices.Specification;

public class CompositeExtendedSpecification<T> : ExtendedSpecification<T>
{
    private Expression<Func<T, bool>> _expression = x => true;

    public void Add(Expression<Func<T, bool>> criterion)
    {
        var parameter = Expression.Parameter(typeof(T));
        var body = Expression.AndAlso(
            Expression.Invoke(_expression, parameter),
            Expression.Invoke(criterion, parameter)
        );
        _expression = Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    public override Expression<Func<T, bool>> ToExpression() => _expression;
}
