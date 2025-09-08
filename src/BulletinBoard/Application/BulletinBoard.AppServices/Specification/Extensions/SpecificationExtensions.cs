using BulletinBoard.AppServices.Specification.LogicalOperations;
using System.Linq;

namespace BulletinBoard.AppServices.Specification.Extensions;

public static class SpecificationExtensions
{
    public static IQueryable<T> ApplyExtendedSpecification<T>(this IQueryable<T> query, ExtendedSpecification<T> specification)
        where T : class
    {
        if (specification == null)
            return query;

        query = query.Where(specification.ToExpression());

        if (specification.OrderBy != null)
            query = query.OrderBy(specification.OrderBy);
        else if (specification.OrderByDescending != null)
            query = query.OrderByDescending(specification.OrderByDescending);

        if (specification.Skip.HasValue)
            query = query.Skip(specification.Skip.Value);

        if (specification.Take.HasValue)
            query = query.Take(specification.Take.Value);

        return query;
    }

    public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, Specification<T> specification)
        where T : class
    {
        return query.Where(specification.ToExpression());
    }
}