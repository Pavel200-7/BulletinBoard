using BulletinBoard.AppServices.Specification.LogicalOperations;
using System.Linq.Expressions;


namespace BulletinBoard.AppServices.Specification;

public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();

    public Specification<T> And(Specification<T> specification)
    {
        return new AndSpecification<T>(this, specification);
    }

    public Specification<T> Or(Specification<T> specification)
    {
        return new OrSpecification<T>(this, specification);
    }

    public Specification<T> Not(Specification<T> specification)
    {
        return new NotSpecification<T>(this);
    }

}
