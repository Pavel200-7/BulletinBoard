using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Specification.LogicalOperations;

public class AndSpecification <T>: Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    public AndSpecification(Specification<T> left, Specification<T> right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> leftExpression = _left.ToExpression();
        Expression<Func<T, bool>> rightExpression = _right.ToExpression();

        ParameterExpression paramExpression = Expression.Parameter(typeof(T));
        BinaryExpression expressionBody = Expression.AndAlso(leftExpression, rightExpression);

        expressionBody = (BinaryExpression)new ParameterReplacer(paramExpression).Visit(expressionBody);

        return Expression.Lambda<Func<T, bool>>(expressionBody, paramExpression);
    }
}
