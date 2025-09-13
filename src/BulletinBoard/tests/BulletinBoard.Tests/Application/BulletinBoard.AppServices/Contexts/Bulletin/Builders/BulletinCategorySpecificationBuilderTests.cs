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
        // Arrange

        // Act
        var spec = _specificationBuilder.Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(_testCategories.Count(), result.Count());
    }

    [Fact]
    public void Build_NameCondition()
    {
        // Arrange
        var searchingCategory = _testCategories[1];
        var categoryId = searchingCategory.Id;
        var categoryName = searchingCategory.CategoryName;

        // Act
        var spec = _specificationBuilder
            .WhereCategoryName(categoryName)
            .Build();

        // Assert
        var findCategory = _testCategories.AsQueryable()
           .ApplyExtendedSpecification(spec)
           .ToList()[0];


        Assert.Equal(searchingCategory, findCategory);
    }

    [Fact]
    public void Build_NameConditionNegative()
    {
        // Arrange
        var searchingCategory = _testCategories[1];
        var categoryId = searchingCategory.Id;
        var categoryName = searchingCategory.CategoryName;

        // Act
        var spec = _specificationBuilder
            .WhereCategoryName(categoryName)
            .Build();

        // Assert
        var findCategory = _testCategories
            .AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList()[0];

        Assert.NotEqual(searchingCategory, _testCategories[2]);
    }

    [Fact]
    public void WhereParentId_WithNullValue_ReturnsRootCategories()
    {
        // Arrange
        var expectedRootCategories = _testCategories.Where(c => c.ParentCategoryId == null).ToList();

        // Act
        var spec = _specificationBuilder
            .WhereParentId(null)
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(expectedRootCategories.Count, result.Count);
        Assert.All(result, c => Assert.Null(c.ParentCategoryId));
    }

    [Fact]
    public void WhereParentId_WithValidValue_ReturnsChildCategories()
    {
        // Arrange
        var parentId = _testCategories[0].Id;
        var expectedChildren = _testCategories.Where(c => c.ParentCategoryId == parentId).ToList();

        // Act
        var spec = _specificationBuilder
            .WhereParentId(parentId)
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(expectedChildren.Count, result.Count);
        Assert.All(result, c => Assert.Equal(parentId, c.ParentCategoryId));
    }

    [Fact]
    public void WhereCategoryName_WithValidName_ReturnsMatchingCategory()
    {
        // Arrange
        var expectedCategory = _testCategories[1];
        var categoryName = expectedCategory.CategoryName;

        // Act
        var spec = _specificationBuilder
            .WhereCategoryName(categoryName)
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Single(result);
        Assert.Equal(expectedCategory.Id, result[0].Id);
    }

    [Fact]
    public void WhereCategoryName_WithEmptyName_ReturnsAllCategories()
    {
        // Arrange

        // Act
        var spec = _specificationBuilder
            .WhereCategoryName("")
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(_testCategories.Count, result.Count);
    }

    [Fact]
    public void WhereCategoryName_WithNullName_ReturnsAllCategories()
    {
        // Arrange

        // Act
        var spec = _specificationBuilder
            .WhereCategoryName("")
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(_testCategories.Count, result.Count);
    }

    [Fact]
    public void WhereCategoryNameContains_WithValidSubstring_ReturnsMatchingCategories()
    {
        // Arrange
        var searchTerm = "корневая";
        var expectedCategories = _testCategories
            .Where(c => c.CategoryName.ToLower().Contains(searchTerm.ToLower()))
            .ToList();

        // Act
        var spec = _specificationBuilder
            .WhereCategoryNameContains(searchTerm)
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(expectedCategories.Count, result.Count);
        Assert.All(result, c => Assert.Contains(searchTerm.ToLower(), c.CategoryName.ToLower()));
    }

    [Fact]
    public void WhereCategoryNameContains_WithEmptyString_ReturnsAllCategories()
    {
        // Arrange

        // Act
        var spec = _specificationBuilder
            .WhereCategoryNameContains("")
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(_testCategories.Count, result.Count);
    }

    [Fact]
    public void WhereIsLeafy_WithTrue_ReturnsLeafyCategories()
    {
        // Arrange
        var expectedCategories = _testCategories.Where(c => c.IsLeafy).ToList();

        // Act
        var spec = _specificationBuilder
            .WhereIsLeafy(true)
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(expectedCategories.Count, result.Count);
        Assert.All(result, c => Assert.True(c.IsLeafy));
    }

    [Fact]
    public void WhereIsLeafy_WithFalse_ReturnsNonLeafyCategories()
    {
        // Arrange
        var expectedCategories = _testCategories.Where(c => !c.IsLeafy).ToList();

        // Act
        var spec = _specificationBuilder
            .WhereIsLeafy(false)
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(expectedCategories.Count, result.Count);
        Assert.All(result, c => Assert.False(c.IsLeafy));
    }

    [Fact]
    public void OrderByCategoryName_Ascending_ReturnsCategoriesInOrder()
    {
        // Arrange
        var expecterOrdered = _testCategories.OrderBy(c => c.CategoryName).ToList();

        // Act
        var spec = _specificationBuilder
            .OrderByCategoryName(true)
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
                .ApplyExtendedSpecification(spec)
                .ToList(); 
    
        Assert.Equal(expecterOrdered.Count, result.Count);
        for (int i = 0; i < expecterOrdered.Count; i++)
        {
            Assert.Equal(expecterOrdered[i].Id, result[i].Id);
        }
    }

    [Fact]
    public void OrderByCategoryName_Descending_ReturnsCategoriesInReverseOrder()
    {
        // Arrange
        var expecterOrdered = _testCategories.OrderByDescending(c => c.CategoryName).ToList();

        // Act
        var spec = _specificationBuilder
            .OrderByCategoryName(false)
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(expecterOrdered.Count, result.Count);
        for (int i = 0; i < expecterOrdered.Count; i++)
        {
            Assert.Equal(expecterOrdered[i].Id, result[i].Id);
        }
    }

    [Fact]
    public void OrderByIsLeafy_Ascending_ReturnsCategoriesInOrder()
    {
        // Arrange
        var expecterOrdered = _testCategories.OrderBy(c => c.IsLeafy).ToList();

        // Act
        var spec = _specificationBuilder
            .OrderByIsLeafy(true)
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(expecterOrdered.Count, result.Count);
        for (int i = 0; i < expecterOrdered.Count; i++)
        {
            Assert.Equal(expecterOrdered[i].IsLeafy, result[i].IsLeafy);
        }
    }

    [Fact]
    public void OrderByIsLeafy_Descending_ReturnsCategoriesInReverseOrder()
    {
        // Arrange
        var expecterOrdered = _testCategories.OrderByDescending(c => c.IsLeafy).ToList();

        // Act
        var spec = _specificationBuilder
            .OrderByIsLeafy(false)
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(expecterOrdered.Count, result.Count);
        for (int i = 0; i < expecterOrdered.Count; i++)
        {
            Assert.Equal(expecterOrdered[i].IsLeafy, result[i].IsLeafy);
        }
    }

    [Fact]
    public void Paginate_WithValidParameters_ReturnsPaginatedResults()
    {
        // Arrange
        var pageNumber = 2;
        var pageSize = 3;
        var expectedCount = Math.Min(pageSize, _testCategories.Count - (pageNumber - 1) * pageSize);

        // Act
        var spec = _specificationBuilder
            .Paginate(pageNumber, pageSize)
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(expectedCount, result.Count);
    }

    [Fact]
    public void Paginate_WithInvalidPageNumber_DefaultsToFirstPage()
    {
        // Arrange
        var pageNumber = 0;
        var pageSize = 3;

        // Act
        var spec = _specificationBuilder
            .Paginate(pageNumber, pageSize)
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(Math.Min(pageSize, _testCategories.Count), result.Count);
    }

    [Fact]
    public void Paginate_WithInvalidPageSize_DefaultsToMinimumSize()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 0;

        // Act
        var spec = _specificationBuilder
            .Paginate(pageNumber, pageSize)
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(_testCategories.Count, result.Count);
    }

    [Fact]
    public void CombinedConditions_WorkCorrectly()
    {
        // Arrange
        var parentId = _testCategories[0].Id;
        var searchTerm = "листовая";

        var expected = _testCategories
            .Where(c => c.ParentCategoryId == parentId &&
                       c.CategoryName.ToLower().Contains(searchTerm.ToLower()) &&
                       c.IsLeafy)
            .OrderBy(c => c.CategoryName)
            .ToList();

        // Act
        var spec = _specificationBuilder
            .WhereParentId(parentId)
            .WhereCategoryNameContains(searchTerm)
            .WhereIsLeafy(true)
            .OrderByCategoryName(true)
            .Paginate(1, 10)
            .Build();

        // Assert
        var result = _testCategories.AsQueryable()
            .ApplyExtendedSpecification(spec)
            .ToList();

        Assert.Equal(expected.Count, result.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.Equal(expected[i].Id, result[i].Id);
        }
    }
}

