using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Builders;

public class BulletinCategorySpecificationBuilder : IBulletinCategorySpecificationBuilder
{

    private CompositeExtendedSpecification<BulletinCategory> _specification;

    public BulletinCategorySpecificationBuilder()
    {
        _specification = new();
    }


    public IBulletinCategorySpecificationBuilder WhereParentId(Guid? parentId)
    {
        if (parentId != null)
        {
            _specification.Add(c => c.ParentCategoryId == parentId.Value);
        }
        else
        {
            _specification.Add(c => c.ParentCategoryId == null);
        }

        return this;
    }

    public IBulletinCategorySpecificationBuilder WhereCategoryName(string categoryName)
    {

        if (!string.IsNullOrEmpty(categoryName))
        {
            _specification.Add(c => c.CategoryName == categoryName);
        }

        return this;
    }

    public IBulletinCategorySpecificationBuilder WhereCategoryNameContains(string categoryName)
    {

        if (!string.IsNullOrEmpty(categoryName))
        {
            _specification.Add(c => c.CategoryName.ToLower().Contains(categoryName.ToLower()));
        }

        return this;
    }

    public IBulletinCategorySpecificationBuilder WhereIsLeafy(bool isLeafy = true)
    {
        _specification.Add(c => c.IsLeafy == isLeafy);
        return this;
    }

    public IBulletinCategorySpecificationBuilder OrderByCategoryName(bool ascending = true)
    {
        if (ascending)
        {
            _specification.OrderBy = x => x.CategoryName;
            _specification.OrderByDescending = null;
        }
        else
        {
            _specification.OrderByDescending = x => x.CategoryName;
            _specification.OrderBy = null;
        }
        return this;
    }

    public IBulletinCategorySpecificationBuilder OrderByIsLeafy(bool ascending = true)
    {
        if (ascending)
        {
            _specification.OrderBy = x => x.IsLeafy;
            _specification.OrderByDescending = null;
        }
        else
        {
            _specification.OrderByDescending = x => x.IsLeafy;
            _specification.OrderBy = null;
        }
        return this;
    }

    public IBulletinCategorySpecificationBuilder Paginate(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        _specification.Skip = (pageNumber - 1) * pageSize;
        _specification.Take = pageSize;
        return this;
    }

    public ExtendedSpecification<BulletinCategory> Build()
    {
        return _specification;
    }

}
