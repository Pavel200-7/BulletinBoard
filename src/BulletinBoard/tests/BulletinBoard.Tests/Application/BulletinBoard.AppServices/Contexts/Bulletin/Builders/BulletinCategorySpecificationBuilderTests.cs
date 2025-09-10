using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders;
using BulletinBoard.AppServices.Specification.Extensions;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.Tests.Application.BulletinBoard.AppServices.Contexts.Bulletin.Builders;

public class BulletinCategorySpecificationBuilderTests
{
    private readonly IBulletinCategorySpecificationBuilder _specificationBuilder;
    private readonly List<BulletinCategory> _testCategories;

    public BulletinCategorySpecificationBuilderTests()
    {
        _specificationBuilder = new BulletinCategorySpecificationBuilder();


        _testCategories = new List<BulletinCategory>();
        _testCategories.Add(new BulletinCategory {Id = Guid.NewGuid(), ParentCategoryId = null, CategoryName = " Категория 1 корневая", IsLeafy = false });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = null, CategoryName = " Категория 2 корневая листовая", IsLeafy = true });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[0].Id, CategoryName = " Категория 3 листовая", IsLeafy = true });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[0].Id, CategoryName = " Категория 4 листовая", IsLeafy = true });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[0].Id, CategoryName = " Категория 5 листовая", IsLeafy = true });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[0].Id, CategoryName = " Категория 6", IsLeafy = false });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[5].Id, CategoryName = " Категория 7 листовая", IsLeafy = true });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[5].Id, CategoryName = " Категория 8 листовая", IsLeafy = true });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[5].Id, CategoryName = " Категория 9 листовая", IsLeafy = true });
        _testCategories.Add(new BulletinCategory { Id = Guid.NewGuid(), ParentCategoryId = _testCategories[5].Id, CategoryName = " Категория 10 листовая", IsLeafy = true });
    }

    [Fact]
    public void Build_WithoutConditions_ReturnsAllItems()
    {
        var spec = _specificationBuilder.Build();


        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();


        Assert.Equal(_testCategories.Count(), result.Count());
    }

    [Fact]
    public void Build_NameCondition()
    {

        var searchingCategory = _testCategories[1];
        var categoryId = searchingCategory.Id;
        var categoryName = searchingCategory.CategoryName;

        var spec = _specificationBuilder
            .WhereCategoryName(categoryName)
            .Build();


        var findCategory = _testCategories.AsQueryable()
           .ApplyExtendedSpecification(spec)
           .ToList()[0];


        Assert.Equal(searchingCategory, findCategory);
    }

    [Fact]
    public void Build_NameConditionNegative()
    {
        var searchingCategory = _testCategories[1];
        var categoryId = searchingCategory.Id;
        var categoryName = searchingCategory.CategoryName;

        var spec = _specificationBuilder
            .WhereCategoryName(categoryName)
            .Build();


        var findCategory = _testCategories
            .AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList()[0];


        Assert.NotEqual(searchingCategory, _testCategories[2]);
    }

    [Fact]
    public void WhereParentId_WithNullValue_ReturnsRootCategories()
    {
        var expectedRootCategories = _testCategories.Where(c => c.ParentCategoryId == null).ToList();

        var spec = _specificationBuilder
            .WhereParentId(null)
            .Build();


        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();


        Assert.Equal(expectedRootCategories.Count, result.Count);
        Assert.All(result, c => Assert.Null(c.ParentCategoryId));
    }

    [Fact]
    public void WhereParentId_WithValidValue_ReturnsChildCategories()
    {
        var parentId = _testCategories[0].Id;
        var expectedChildren = _testCategories.Where(c => c.ParentCategoryId == parentId).ToList();

        var spec = _specificationBuilder
            .WhereParentId(parentId)
            .Build();


        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();


        Assert.Equal(expectedChildren.Count, result.Count);
        Assert.All(result, c => Assert.Equal(parentId, c.ParentCategoryId));
    }

    [Fact]
    public void WhereCategoryName_WithValidName_ReturnsMatchingCategory()
    {
        var expectedCategory = _testCategories[1];
        var categoryName = expectedCategory.CategoryName;

        var spec = _specificationBuilder
            .WhereCategoryName(categoryName)
            .Build();

        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();


        Assert.Single(result);
        Assert.Equal(expectedCategory.Id, result[0].Id);
    }

    [Fact]
    public void WhereCategoryName_WithEmptyName_ReturnsAllCategories()
    {
        var spec = _specificationBuilder
            .WhereCategoryName("")
            .Build();


        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();


        Assert.Equal(_testCategories.Count, result.Count);
    }

    [Fact]
    public void WhereCategoryName_WithNullName_ReturnsAllCategories()
    {
        var spec = _specificationBuilder
            .WhereCategoryName(null)
            .Build();


        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();


        Assert.Equal(_testCategories.Count, result.Count);
    }

    [Fact]
    public void WhereCategoryNameContains_WithValidSubstring_ReturnsMatchingCategories()
    {
        var searchTerm = "корневая";
        var expectedCategories = _testCategories
            .Where(c => c.CategoryName.ToLower().Contains(searchTerm.ToLower()))
            .ToList();

        var spec = _specificationBuilder
            .WhereCategoryNameContains(searchTerm)
            .Build();


        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();


        Assert.Equal(expectedCategories.Count, result.Count);
        Assert.All(result, c => Assert.Contains(searchTerm.ToLower(), c.CategoryName.ToLower()));
    }

    [Fact]
    public void WhereCategoryNameContains_WithEmptyString_ReturnsAllCategories()
    {
        var spec = _specificationBuilder
            .WhereCategoryNameContains("")
            .Build();


        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();


        Assert.Equal(_testCategories.Count, result.Count);
    }

    [Fact]
    public void WhereIsLeafy_WithTrue_ReturnsLeafyCategories()
    {
        var expectedCategories = _testCategories.Where(c => c.IsLeafy).ToList();


        var spec = _specificationBuilder
            .WhereIsLeafy(true)
            .Build();

        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();


        Assert.Equal(expectedCategories.Count, result.Count);
        Assert.All(result, c => Assert.True(c.IsLeafy));
    }

    [Fact]
    public void WhereIsLeafy_WithFalse_ReturnsNonLeafyCategories()
    {
        var expectedCategories = _testCategories.Where(c => !c.IsLeafy).ToList();


        var spec = _specificationBuilder
            .WhereIsLeafy(false)
            .Build();

        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();


        Assert.Equal(expectedCategories.Count, result.Count);
        Assert.All(result, c => Assert.False(c.IsLeafy));
    }

    [Fact]
    public void OrderByCategoryName_Ascending_ReturnsCategoriesInOrder()
    {
        var spec = _specificationBuilder
            .OrderByCategoryName(true)
            .Build();

        var result = _testCategories.AsQueryable()
                .ApplyExtendedSpecification(spec)
                .ToList(); 
    
        var ordered = _testCategories.OrderBy(c => c.CategoryName).ToList();


        Assert.Equal(ordered.Count, result.Count);
        for (int i = 0; i < ordered.Count; i++)
        {
            Assert.Equal(ordered[i].Id, result[i].Id);
        }
    }

    [Fact]
    public void OrderByCategoryName_Descending_ReturnsCategoriesInReverseOrder()
    {
        var spec = _specificationBuilder
            .OrderByCategoryName(false)
            .Build();

        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        var ordered = _testCategories.OrderByDescending(c => c.CategoryName).ToList();


        Assert.Equal(ordered.Count, result.Count);
        for (int i = 0; i < ordered.Count; i++)
        {
            Assert.Equal(ordered[i].Id, result[i].Id);
        }
    }

    [Fact]
    public void OrderByIsLeafy_Ascending_ReturnsCategoriesInOrder()
    {
        var spec = _specificationBuilder
            .OrderByIsLeafy(true)
            .Build();


        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        var ordered = _testCategories.OrderBy(c => c.IsLeafy).ToList();


        Assert.Equal(ordered.Count, result.Count);
        for (int i = 0; i < ordered.Count; i++)
        {
            Assert.Equal(ordered[i].IsLeafy, result[i].IsLeafy);
        }
    }

    [Fact]
    public void OrderByIsLeafy_Descending_ReturnsCategoriesInReverseOrder()
    {
        var spec = _specificationBuilder
            .OrderByIsLeafy(false)
            .Build();

        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        var ordered = _testCategories.OrderByDescending(c => c.IsLeafy).ToList();


        Assert.Equal(ordered.Count, result.Count);
        for (int i = 0; i < ordered.Count; i++)
        {
            Assert.Equal(ordered[i].IsLeafy, result[i].IsLeafy);
        }
    }

    [Fact]
    public void Paginate_WithValidParameters_ReturnsPaginatedResults()
    {
        var pageNumber = 2;
        var pageSize = 3;
        var expectedCount = Math.Min(pageSize, _testCategories.Count - (pageNumber - 1) * pageSize);

        var spec = _specificationBuilder
            .Paginate(pageNumber, pageSize)
            .Build();


        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(expectedCount, result.Count);
    }

    [Fact]
    public void Paginate_WithInvalidPageNumber_DefaultsToFirstPage()
    {
        var pageNumber = 0; // Invalid
        var pageSize = 3;

        var spec = _specificationBuilder
            .Paginate(pageNumber, pageSize)
            .Build();

        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();


        Assert.Equal(Math.Min(pageSize, _testCategories.Count), result.Count);
    }

    [Fact]
    public void Paginate_WithInvalidPageSize_DefaultsToMinimumSize()
    {
        var pageNumber = 1;
        var pageSize = 0;

        var spec = _specificationBuilder
            .Paginate(pageNumber, pageSize)
            .Build();


        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();


        Assert.Equal(_testCategories.Count, result.Count);
    }

    [Fact]
    public void CombinedConditions_WorkCorrectly()
    {
        var parentId = _testCategories[0].Id;
        var searchTerm = "листовая";

        var spec = _specificationBuilder
            .WhereParentId(parentId)
            .WhereCategoryNameContains(searchTerm)
            .WhereIsLeafy(true)
            .OrderByCategoryName(true)
            .Paginate(1, 10)
            .Build();


        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();


        var expected = _testCategories
            .Where(c => c.ParentCategoryId == parentId &&
                       c.CategoryName.ToLower().Contains(searchTerm.ToLower()) &&
                       c.IsLeafy)
            .OrderBy(c => c.CategoryName)
            .ToList();


        Assert.Equal(expected.Count, result.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.Equal(expected[i].Id, result[i].Id);
        }
    }
}

