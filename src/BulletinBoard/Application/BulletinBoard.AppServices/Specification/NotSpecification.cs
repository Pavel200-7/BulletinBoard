using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Specification
{
    public class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;

        public NotSpecification(Specification<T> left)
        {
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            
            UnaryExpression NotBody = Expression.Not(leftExpression.Body);
            ParameterExpression param = leftExpression.Parameters[0];

            return Expression.Lambda<Func<T, bool>>(NotBody, param);
        }

    }
}
