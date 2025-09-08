using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.Domain.Entities.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builder
{
    public class BulletinCategoryPredicateBuilder : IBulletinCategoryPredicateBuilder
    {

        private IQueryable<BulletinCategory> _query;

        public BulletinCategoryPredicateBuilder(IQueryable<BulletinCategory> query)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public IBulletinCategoryPredicateBuilder WhereParentId(Guid? parentId)
        {
            if (parentId != null)
            {
                _query = _query.Where(c => c.ParentCategoryId == parentId.Value);
            }
            else
            {
                _query = _query.Where(c => c.ParentCategoryId == null);
            }

            return this;
        }

        public IBulletinCategoryPredicateBuilder WhereCategoryName(string categoryName)
        {

            if (!String.IsNullOrEmpty(categoryName))
            {
                _query = _query.Where(c => c.CategoryName == categoryName);
            }

            return this;
        }

        public IBulletinCategoryPredicateBuilder WhereCategoryNameContains(string categoryName)
        {

            if (!String.IsNullOrEmpty(categoryName))
            {
                _query = _query.Where(c => c.CategoryName.ToLower().Contains(categoryName.ToLower()));
            }

            return this;
        }

        public IBulletinCategoryPredicateBuilder WhereIsLeafy(bool isLeafy = true)
        {
            _query = _query.Where(c => c.IsLeafy == isLeafy);
            return this;
        }

        public IBulletinCategoryPredicateBuilder OrderByCategoryName(bool ascending = true)
        {
            if (ascending)
            {
                _query = _query.OrderBy(c => c.CategoryName);
            }
            else
            {
                _query = _query.OrderByDescending(c => c.CategoryName);
            }

            return this;
        }

        public IBulletinCategoryPredicateBuilder OrderByIsLeafy(bool ascending = true)
        {
            if (ascending)
            {
                _query = _query.OrderBy(c => c.IsLeafy);
            }
            else
            {
                _query = _query.OrderByDescending(c => c.IsLeafy);
            }

            return this;
        }

        public IBulletinCategoryPredicateBuilder Paginate(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            _query = _query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return this;
        }

        public IQueryable<BulletinCategory> Build()
        {
            return _query;
        }

    }
}
