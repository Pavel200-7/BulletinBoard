using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Specifications.BulletinCategorySpecifications
{
    public class BulletinCategoryNameSpecification : Specification<BulletinCategory>
    {
        private readonly string _categoryName;

        public BulletinCategoryNameSpecification(string categoryName)
        {
            _categoryName = categoryName;
        }

        public override Expression<Func<BulletinCategory, bool>> ToExpression()
        {
            return c => c.CategoryName == _categoryName;
        }
    }
}
