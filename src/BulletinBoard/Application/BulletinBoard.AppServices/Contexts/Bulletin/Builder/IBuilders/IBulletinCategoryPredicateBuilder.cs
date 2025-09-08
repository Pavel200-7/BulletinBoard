using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;

public interface IBulletinCategoryPredicateBuilder
{
    public IBulletinCategoryPredicateBuilder WhereParentId(Guid? parentId);
    
    public IBulletinCategoryPredicateBuilder WhereCategoryName(string categoryName);

    public IBulletinCategoryPredicateBuilder WhereCategoryNameContains(string categoryName);

    public IBulletinCategoryPredicateBuilder WhereIsLeafy(bool isLeafy);

    public IBulletinCategoryPredicateBuilder OrderByCategoryName(bool ascending = true);

    public IBulletinCategoryPredicateBuilder OrderByIsLeafy(bool ascending = true);

    public IBulletinCategoryPredicateBuilder Paginate(int pageNumber, int pageSize);

    public IQueryable<BulletinCategory> Build();
}