using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.ReadDto;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.UpdateDto;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text;
using Xunit.Abstractions;

namespace BulletinBoard.IntegrationTests.Hosts.Controllers;

public class BulletinCategoryControllerTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    public readonly CustomWebApplicationFactory _factory;
    public readonly HttpClient Client;
    private readonly ITestOutputHelper _output;
    private readonly IServiceScope _scope;
    private readonly BulletinContext _dbContext;


    public BulletinCategoryControllerTests
        (
        CustomWebApplicationFactory factory,
        ITestOutputHelper output
        )
    {
        _factory = factory;
        Client = _factory.CreateClient();

        _output = output;

        _scope = _factory.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<BulletinContext>();
    }

    public async Task InitializeAsync()
    {
        await ClearDatabase();
    }

    public async Task DisposeAsync()
    {
        await ClearDatabase();
        _scope?.Dispose();
    }

    private async Task ClearDatabase()
    {
        // Правильное название свойства - BulletinCategories (множественное число)
        _dbContext.BulletinCategory.RemoveRange(_dbContext.BulletinCategory);
        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task GetBulletinCategory_Positive()
    {
        // Arrange
        string url = "/api/BulletinCategory";
        BulletinCategoryCreateDto createDto = new()
        {
            ParentCategoryId = null,
            CategoryName = "Тестовая категория",
            IsLeafy = true
        };

        var json = JsonConvert.SerializeObject(createDto);
        var context = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(url, context);


        var responceJson = await response.Content.ReadAsStringAsync();
        var createdCategory = JsonConvert.DeserializeObject<BulletinCategoryDto>(responceJson);
        Guid categoryId = createdCategory.Id;

        // Act
        var getStopwatch = Stopwatch.StartNew();

        string getUrl = $"{url}/{categoryId}";
        response = await Client.GetAsync(getUrl);


        responceJson = await response.Content.ReadAsStringAsync();
        var reseivedCategory = JsonConvert.DeserializeObject<BulletinCategoryDto>(responceJson);


        _output.WriteLine($"    Время выполнения: {getStopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(createDto.CategoryName, reseivedCategory.CategoryName);
    }

    [Fact]
    public async Task GetBulletinCategory_Negative()
    {
        // Arrange
        string url = "/api/BulletinCategory";
        BulletinCategoryCreateDto createDto = new()
        {
            ParentCategoryId = null,
            CategoryName = "Тестовая категория",
            IsLeafy = true
        };

        Guid categoryId = Guid.NewGuid();

        // Act
        var getStopwatch = Stopwatch.StartNew();

        string getUrl = $"{url}/{categoryId}";
        var response = await Client.GetAsync(getUrl);


        var responceJson = await response.Content.ReadAsStringAsync();
        var reseivedCategory = JsonConvert.DeserializeObject<BulletinCategoryDto>(responceJson);

        _output.WriteLine($"    Время выполнения: {getStopwatch.ElapsedMilliseconds} мс");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateBulletinCategory_Positive()
    {
        // Arrange
        string url = "/api/BulletinCategory";
        BulletinCategoryCreateDto createDto = new()
        {
            ParentCategoryId = null,
            CategoryName = "Тестовая категория",
            IsLeafy = true
        };

        var json = JsonConvert.SerializeObject(createDto);
        var context = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var getStopwatch = Stopwatch.StartNew();

        var response = await Client.PostAsync(url, context);


        var responceJson = await response.Content.ReadAsStringAsync();
        var createdCategory = JsonConvert.DeserializeObject<BulletinCategoryDto>(responceJson);

        _output.WriteLine($"    Время выполнения: {getStopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(createdCategory.CategoryName, createDto.CategoryName);
    }

    [Theory]
    [MemberData(nameof(CreateBulletinCategory_Negative_DataSorse))]
    public async Task CreateBulletinCategory_Negative(BulletinCategoryCreateDto createDto)
    {
        // Arrange
        string url = "/api/BulletinCategory";
        
        var json = JsonConvert.SerializeObject(createDto);
        var context = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var getStopwatch = Stopwatch.StartNew();

        var response = await Client.PostAsync(url, context);

        _output.WriteLine($"    Время выполнения: {getStopwatch.ElapsedMilliseconds} мс");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    }

    public static IEnumerable<object[]> CreateBulletinCategory_Negative_DataSorse()
    {
        // Короткое название.
        yield return new object[]
        {
            new BulletinCategoryCreateDto()
            {
                ParentCategoryId = null,
                CategoryName = "Те",
                IsLeafy = false
            }
        };

        // Неизвестная родительская категория.
        yield return new object[]
        {
            new BulletinCategoryCreateDto()
            {
                ParentCategoryId =  Guid.NewGuid(),
                CategoryName = "Тестовая категория",
                IsLeafy = false
            }
        };

        // Запрешенные символы в имени.
        yield return new object[]
        {
            new BulletinCategoryCreateDto()
            {
                ParentCategoryId = null,
                CategoryName = "Тестовая категория.",
                IsLeafy = false
            }
        };
    }

    [Fact]
    public async Task UpdateBulletinCategory_Positive()
    {
        // Arrange
        string url = "/api/BulletinCategory";
        BulletinCategoryCreateDto createDto = new()
        {
            ParentCategoryId = null,
            CategoryName = "Тестовая категория",
            IsLeafy = true
        };

        BulletinCategoryUpdateDto updateDto = new()
        {
            ParentCategoryId = null,
            CategoryName = "Тестовая категория измененная"
        };

        var json = JsonConvert.SerializeObject(createDto);
        var context = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(url, context);


        var responceJson = await response.Content.ReadAsStringAsync();
        var createdCategory = JsonConvert.DeserializeObject<BulletinCategoryDto>(responceJson);
        Guid categoryId = createdCategory.Id;

        // Act
        var getStopwatch = Stopwatch.StartNew();

        string putUrl = $"{url}/{categoryId}";



        json = JsonConvert.SerializeObject(updateDto);
        context = new StringContent(json, Encoding.UTF8, "application/json");

        response = await Client.PutAsync(putUrl, context);

        responceJson = await response.Content.ReadAsStringAsync();
        var reseivedCategory = JsonConvert.DeserializeObject<BulletinCategoryDto>(responceJson);

        _output.WriteLine($"    Время выполнения: {getStopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(updateDto.CategoryName, reseivedCategory.CategoryName);
    }

    [Theory]
    [MemberData(nameof(UpdateBulletinCategory_Negative_DataSorse))]
    public async Task UpdateBulletinCategory_Negative(BulletinCategoryCreateDto createDto, BulletinCategoryUpdateDto updateDto, HttpStatusCode expecteStatusCode)
    {
        // Arrange
        string url = "/api/BulletinCategory";

        var json = JsonConvert.SerializeObject(createDto);
        var context = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(url, context);


        var responceJson = await response.Content.ReadAsStringAsync();
        var createdCategory = JsonConvert.DeserializeObject<BulletinCategoryDto>(responceJson);
        Guid categoryId = createdCategory is null ? Guid.NewGuid() : createdCategory.Id;

        // Act
        var getStopwatch = Stopwatch.StartNew();

        string putUrl = $"{url}/{categoryId}";



        json = JsonConvert.SerializeObject(updateDto);
        context = new StringContent(json, Encoding.UTF8, "application/json");

        response = await Client.PutAsync(putUrl, context);

        responceJson = await response.Content.ReadAsStringAsync();
        var reseivedCategory = JsonConvert.DeserializeObject<BulletinCategoryDto>(responceJson);

        _output.WriteLine($"    Время выполнения: {getStopwatch.ElapsedMilliseconds} мс");

        // Assert
        Assert.Equal(expecteStatusCode, response.StatusCode);
    }

    public static IEnumerable<object[]> UpdateBulletinCategory_Negative_DataSorse()
    {
        // Короткое название при создании.
        yield return new object[]
        {
            new BulletinCategoryCreateDto()
            {
                ParentCategoryId = null,
                CategoryName = "Тестовая категория",
                IsLeafy = false
            },
            new BulletinCategoryUpdateDto()
            {
                ParentCategoryId = null,
                CategoryName = "Те"
            },
            HttpStatusCode.BadRequest
        };

        // Неизвестная родительская категория.
        yield return new object[]
       {
            new BulletinCategoryCreateDto()
            {
                ParentCategoryId = null,
                CategoryName = "Тe",
                IsLeafy = false
            },
            new BulletinCategoryUpdateDto()
            {
                ParentCategoryId = null,
                CategoryName = "Тестовая категория"
            },
            HttpStatusCode.NotFound
       };
    }

    [Fact]
    public async Task DeleteBulletinCategory_Positive()
    {
        // Arrange
        string url = "/api/BulletinCategory";
        BulletinCategoryCreateDto createDto = new()
        {
            ParentCategoryId = null,
            CategoryName = "Тестовая категория",
            IsLeafy = true
        };


        var json = JsonConvert.SerializeObject(createDto);
        var context = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(url, context);


        var responceJson = await response.Content.ReadAsStringAsync();
        var createdCategory = JsonConvert.DeserializeObject<BulletinCategoryDto>(responceJson);
        Guid categoryId = createdCategory.Id;

        // Act
        var getStopwatch = Stopwatch.StartNew();

        string deleteUrl = $"{url}/{categoryId}";

        response = await Client.DeleteAsync(deleteUrl);

        responceJson = await response.Content.ReadAsStringAsync();
        var reseivedRes = JsonConvert.DeserializeObject<bool>(responceJson);

        _output.WriteLine($"    Время выполнения: {getStopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        bool expectedRes = true;
        Assert.Equal(expectedRes, reseivedRes);
    }

    [Fact]
    public async Task DeleteBulletinCategory_Negative()
    {
        // Arrange
        string url = "/api/BulletinCategory";
        
        Guid categoryId = Guid.NewGuid();

        // Act
        var getStopwatch = Stopwatch.StartNew();

        string deleteUrl = $"{url}/{categoryId}";

        var response = await Client.DeleteAsync(deleteUrl);


        _output.WriteLine($"    Время выполнения: {getStopwatch.ElapsedMilliseconds} мс");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetAllBulletinCategory_Positive()
    {
        // Arrange
        string url = "/api/BulletinCategory";
        BulletinCategoryCreateDto createDto = new()
        {
            ParentCategoryId = null,
            CategoryName = "Тестовая категория один",
            IsLeafy = false
        };

        var json = JsonConvert.SerializeObject(createDto);
        var context = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await Client.PostAsync(url, context);

        var responceJson = await response.Content.ReadAsStringAsync();
        var createdCategory = JsonConvert.DeserializeObject<BulletinCategoryDto>(responceJson);
        Guid categoryId = createdCategory.Id;

        BulletinCategoryCreateDto createDto2 = new()
        {
            ParentCategoryId = categoryId,
            CategoryName = "Тестовая категория два",
            IsLeafy = true
        };

        json = JsonConvert.SerializeObject(createDto2);
        context = new StringContent(json, Encoding.UTF8, "application/json");
        response = await Client.PostAsync(url, context);

        responceJson = await response.Content.ReadAsStringAsync();
        createdCategory = JsonConvert.DeserializeObject<BulletinCategoryDto>(responceJson);
        categoryId = createdCategory.Id;

        // Act
        var getStopwatch = Stopwatch.StartNew();

        string GetUrl = $"{url}";

        response = await Client.GetAsync(GetUrl);

        responceJson = await response.Content.ReadAsStringAsync();
        var foundCategoryes = JsonConvert.DeserializeObject<BulletinCategoryReadAllDto>(responceJson);

        var secondCategory = foundCategoryes.ChildrenCategories.First().ChildrenCategories.First();

        _output.WriteLine($"    Время выполнения: {getStopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(createDto2.CategoryName, secondCategory.CategoryName);
    }

    [Fact]
    public async Task GetAllBulletinCategory_Negative()
    {
        // Входных данных нет
        Assert.Equal(true, true);

    }

    [Fact]
    public async Task GetsingleBulletinCategory_Positive()
    {
        // Arrange
        string url = "/api/BulletinCategory";
        BulletinCategoryCreateDto createDto = new()
        {
            ParentCategoryId = null,
            CategoryName = "Тестовая категория один",
            IsLeafy = false
        };

        var json = JsonConvert.SerializeObject(createDto);
        var context = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await Client.PostAsync(url, context);

        var responceJson = await response.Content.ReadAsStringAsync();
        var createdCategory = JsonConvert.DeserializeObject<BulletinCategoryDto>(responceJson);
        Guid categoryId = createdCategory.Id;

        BulletinCategoryCreateDto createDto2 = new()
        {
            ParentCategoryId = categoryId,
            CategoryName = "Тестовая категория два",
            IsLeafy = true
        };

        json = JsonConvert.SerializeObject(createDto2);
        context = new StringContent(json, Encoding.UTF8, "application/json");
        response = await Client.PostAsync(url, context);


        responceJson = await response.Content.ReadAsStringAsync();
        createdCategory = JsonConvert.DeserializeObject<BulletinCategoryDto>(responceJson);
        Guid category2Id = createdCategory.Id;

        // Act
        var getStopwatch = Stopwatch.StartNew();

        string GetUrl = $"{url}/{category2Id}/SingleChain";

        response = await Client.GetAsync(GetUrl);

        responceJson = await response.Content.ReadAsStringAsync();
        var foundCategoryes = JsonConvert.DeserializeObject<BulletinCategoryReadSingleDto>(responceJson);

        var secondCategory = foundCategoryes.ChildrenCategory;

        _output.WriteLine($"    Время выполнения: {getStopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(createDto2.CategoryName, secondCategory.CategoryName);
    }

    [Fact]
    public async Task GetsingleBulletinCategory_Negative()
    {
        // Arrange
        string url = "/api/BulletinCategory";
        Guid unKnownId = Guid.NewGuid();

        // Act
        var getStopwatch = Stopwatch.StartNew();

        string GetUrl = $"{url}/SingleChain/{unKnownId}";

        var response = await Client.GetAsync(GetUrl);

        _output.WriteLine($"    Время выполнения: {getStopwatch.ElapsedMilliseconds} мс");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
