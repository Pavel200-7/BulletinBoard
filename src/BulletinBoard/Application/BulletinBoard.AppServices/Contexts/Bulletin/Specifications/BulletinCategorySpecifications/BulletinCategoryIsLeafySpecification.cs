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
    public class BulletinCategoryIsLeafySpecification : Specification<BulletinCategory>
    {
        private readonly bool _isLeafy;

        public BulletinCategoryIsLeafySpecification(bool isLeafy)
        {
            _isLeafy = isLeafy;
        }

        public override Expression<Func<BulletinCategory, bool>> ToExpression()
        {
            return c => c.IsLeafy == _isLeafy;
        }
    }
}
