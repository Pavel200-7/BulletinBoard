using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Specification;

public abstract class ExtendedSpecification<T> : Specification<T>
{
    public Expression<Func<T, object>>? OrderBy { get; set; }

    public Expression<Func<T, object>>? OrderByDescending { get; set; }

    public int? Skip { get; set; }

    public int? Take { get; set; }

    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
}
