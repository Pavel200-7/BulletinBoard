using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;

public interface IBulletinCategorySpecificationBuilder
{
    public IBulletinCategorySpecificationBuilder WhereParentId(Guid? parentId);
    
    public IBulletinCategorySpecificationBuilder WhereCategoryName(string categoryName);

    public IBulletinCategorySpecificationBuilder WhereCategoryNameContains(string categoryName);

    public IBulletinCategorySpecificationBuilder WhereIsLeafy(bool isLeafy);

    public IBulletinCategorySpecificationBuilder OrderByCategoryName(bool ascending = true);

    public IBulletinCategorySpecificationBuilder OrderByIsLeafy(bool ascending = true);

    public IBulletinCategorySpecificationBuilder Paginate(int pageNumber, int pageSize);

    public ExtendedSpecification<BulletinCategory> Build();
}