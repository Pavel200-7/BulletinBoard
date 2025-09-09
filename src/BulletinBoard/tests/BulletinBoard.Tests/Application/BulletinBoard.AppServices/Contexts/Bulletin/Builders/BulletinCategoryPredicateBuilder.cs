using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders;
using BulletinBoard.Domain.Entities.Bulletin;
using Xunit;


namespace BulletinBoard.Tests.Application.BulletinBoard.AppServices.Contexts.Bulletin.Builders;

public class BulletinCategoryPredicateBuilderTests
{
    private readonly IBulletinCategorySpecificationBuilder _specificationBuilder;
    private readonly List<BulletinCategory> _testCategories;

    public BulletinCategoryPredicateBuilderTests()
    {
        _specificationBuilder = new BulletinCategorySpecificationBuilder();


        _testCategories = new List<BulletinCategory>();
        _testCategories.Add(new BulletinCategory {Id = Guid.NewGuid(), ParentCategoryId = null, CategoryName = " Категория 1 корневая", IsLeafy = false });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = null, CategoryName = " Категория 2 корневая листовая", IsLeafy = true });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[0].Id, CategoryName = " Категория 3 листовая", IsLeafy = true });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[0].Id, CategoryName = " Категория 4 листовая", IsLeafy = true });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[0].Id, CategoryName = " Категория 5 листовая", IsLeafy = true });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[0].Id, CategoryName = " Категория 5", IsLeafy = false });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[5].Id, CategoryName = " Категория 6 листовая", IsLeafy = true });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[5].Id, CategoryName = " Категория 7 листовая", IsLeafy = true });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[5].Id, CategoryName = " Категория 8 листовая", IsLeafy = true });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[5].Id, CategoryName = " Категория 9 листовая", IsLeafy = true });
    }

    [Fact]
    public void Build_WithoutConditions_ReturnsAllItems()
    {
        var spec = _specificationBuilder.Build();

        var result = _testCategories.AsQueryable().Where(spec.ToExpression());

        Assert.Equal(_testCategories.Count(), result.Count());
    }

}
