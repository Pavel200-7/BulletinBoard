using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.CreateDto;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace BulletinBoard.IntegrationTests.Hosts.Controllers;

public class BulletinCharacteristicComparisonControllerTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;
    private readonly IServiceScope _scope;
    private readonly BulletinContext _dbContext;

    public BulletinCharacteristicComparisonControllerTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _factory = factory;
        _client = _factory.CreateClient();
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
        _dbContext.BulletinCharacteristicСomparison.RemoveRange(_dbContext.BulletinCharacteristicСomparison);
        _dbContext.BulletinImage.RemoveRange(_dbContext.BulletinImage);
        _dbContext.BulletinViewsCount.RemoveRange(_dbContext.BulletinViewsCount);
        _dbContext.BulletinMain.RemoveRange(_dbContext.BulletinMain);
        _dbContext.BulletinCharacteristicValue.RemoveRange(_dbContext.BulletinCharacteristicValue);
        _dbContext.BulletinCharacteristic.RemoveRange(_dbContext.BulletinCharacteristic);
        _dbContext.BulletinCategory.RemoveRange(_dbContext.BulletinCategory);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<Guid> CreateTestCategoryAsync(string categoryName = "Тестовая категория", bool isLeafy = true)
    {
        var createCategoryDto = new BulletinCategoryCreateDto
        {
            ParentCategoryId = null,
            CategoryName = categoryName,
            IsLeafy = isLeafy
        };

        var response = await _client.PostAsJsonAsync("/api/BulletinCategory", createCategoryDto);
        response.EnsureSuccessStatusCode();

        var createdCategory = await response.Content.ReadFromJsonAsync<BulletinCategoryDto>();
        return createdCategory.Id;
    }

    private async Task<Guid> CreateTestCharacteristicAsync(Guid categoryId, string characteristicName = "Тестовая характеристика")
    {
        var createDto = new BulletinCharacteristicCreateDto
        {
            Name = characteristicName,
            CategoryId = categoryId
        };

        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristic", createDto);
        response.EnsureSuccessStatusCode();

        var createdCharacteristic = await response.Content.ReadFromJsonAsync<BulletinCharacteristicDto>();
        return createdCharacteristic.Id;
    }

    private async Task<Guid> CreateTestCharacteristicValueAsync(Guid characteristicId, string value = "Тестовое значение")
    {
        var createDto = new BulletinCharacteristicValueCreateDto
        {
            CharacteristicId = characteristicId,
            Value = value
        };

        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristicValue", createDto);
        response.EnsureSuccessStatusCode();

        var createdValue = await response.Content.ReadFromJsonAsync<BulletinCharacteristicValueDto>();
        return createdValue.Id;
    }

    private async Task<Guid> CreateTestUserAsync()
    {
        var newUser = new BulletinUser()
        {
            Id = Guid.NewGuid(),
            FullName = "TestName",
            FormattedAddress = "TestAdress",
            Latitude = 2.22112,
            Longitude = 2.22112,
            Phone = "11111111111"
        };
        var createdUser = await _dbContext.BulletinUser.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();
        return createdUser.Entity.Id;
    }

    private async Task<Guid> CreateTestBulletinAsync(Guid userId, Guid categoryId)
    {
        var createDto = new BulletinCreateDtoRequest
        {
            BulletinMain = new BulletinMainCreateDto
            {
                UserId = userId,
                Title = "Тестовое объявление",
                Description = "Это тестовое описание объявления",
                CategoryId = categoryId,
                Price = 1000.50m
            },
            CharacteristicComparisons = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>()
        };

        var response = await _client.PostAsJsonAsync("/api/Bulletin", createDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    private async Task<Guid> CreateTestCharacteristicComparisonAsync(Guid bulletinId, Guid characteristicId, Guid characteristicValueId)
    {
        var createDto = new BulletinCharacteristicComparisonCreateDto
        {
            BulletinId = bulletinId,
            CharacteristicId = characteristicId,
            CharacteristicValueId = characteristicValueId
        };

        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristicComparison", createDto);
        response.EnsureSuccessStatusCode();

        var createdComparison = await response.Content.ReadFromJsonAsync<BulletinCharacteristicComparisonDto>();
        return createdComparison.Id;
    }

    [Fact]
    public async Task GetById_Positive()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();
        var bulletinId = await CreateTestBulletinAsync(userId, categoryId);
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var characteristicValueId = await CreateTestCharacteristicValueAsync(characteristicId);
        var comparisonId = await CreateTestCharacteristicComparisonAsync(bulletinId, characteristicId, characteristicValueId);

        // Act
        var response = await _client.GetAsync($"/api/BulletinCharacteristicComparison/{comparisonId}");
        var comparison = await response.Content.ReadFromJsonAsync<BulletinCharacteristicComparisonDto>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(comparison);
        Assert.Equal(comparisonId, comparison.Id);
        Assert.Equal(bulletinId, comparison.BulletinId);
        Assert.Equal(characteristicId, comparison.CharacteristicId);
        Assert.Equal(characteristicValueId, comparison.CharacteristicValueId);
    }

    [Fact]
    public async Task GetById_Negative()
    {
        // Arrange
        var unknownId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/BulletinCharacteristicComparison/{unknownId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Create_Positive()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();
        var bulletinId = await CreateTestBulletinAsync(userId, categoryId);
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var characteristicValueId = await CreateTestCharacteristicValueAsync(characteristicId);

        var createDto = new BulletinCharacteristicComparisonCreateDto
        {
            BulletinId = bulletinId,
            CharacteristicId = characteristicId,
            CharacteristicValueId = characteristicValueId
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristicComparison", createDto);
        var createdComparison = await response.Content.ReadFromJsonAsync<BulletinCharacteristicComparisonDto>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(createdComparison);
        Assert.Equal(createDto.BulletinId, createdComparison.BulletinId);
        Assert.Equal(createDto.CharacteristicId, createdComparison.CharacteristicId);
        Assert.Equal(createDto.CharacteristicValueId, createdComparison.CharacteristicValueId);
        Assert.NotEqual(Guid.Empty, createdComparison.Id);
    }

    [Theory]
    [MemberData(nameof(Create_Negative_DataSource))]
    public async Task Create_Negative(BulletinCharacteristicComparisonCreateDto createDto)
    {
        // Act
        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristicComparison", createDto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public static IEnumerable<object[]> Create_Negative_DataSource()
    {
        // Несуществующее id
        yield return new object[]
        {
            new BulletinCharacteristicComparisonCreateDto
            {
                BulletinId = Guid.NewGuid(),
                CharacteristicId = Guid.NewGuid(),
                CharacteristicValueId = Guid.NewGuid()
            }
        };
    }

    [Fact]
    public async Task Update_Positive()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();
        var bulletinId = await CreateTestBulletinAsync(userId, categoryId);
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);

        // Создаем два значения характеристики
        var value1Id = await CreateTestCharacteristicValueAsync(characteristicId, "Значение 1");
        var value2Id = await CreateTestCharacteristicValueAsync(characteristicId, "Значение 2");

        var comparisonId = await CreateTestCharacteristicComparisonAsync(bulletinId, characteristicId, value1Id);

        var updateDto = new BulletinCharacteristicComparisonUpdateDto
        {
            CharacteristicValueId = value2Id
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/BulletinCharacteristicComparison/{comparisonId}", updateDto);
        var updatedComparison = await response.Content.ReadFromJsonAsync<BulletinCharacteristicComparisonDto>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(updatedComparison);
        Assert.Equal(comparisonId, updatedComparison.Id);
        Assert.Equal(updateDto.CharacteristicValueId, updatedComparison.CharacteristicValueId);
        // Проверяем что остальные поля не изменились
        Assert.Equal(bulletinId, updatedComparison.BulletinId);
        Assert.Equal(characteristicId, updatedComparison.CharacteristicId);
    }

    [Fact]
    public async Task Update_NotFound()
    {
        // Arrange
        var updateDto = new BulletinCharacteristicComparisonUpdateDto
        {
            CharacteristicValueId = Guid.NewGuid()
        };
        var unknownId = Guid.NewGuid();

        // Act
        var response = await _client.PutAsJsonAsync($"/api/BulletinCharacteristicComparison/{unknownId}", updateDto);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Update_InvalidCharacteristicValue_Negative()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();
        var bulletinId = await CreateTestBulletinAsync(userId, categoryId);
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var valueId = await CreateTestCharacteristicValueAsync(characteristicId);
        var comparisonId = await CreateTestCharacteristicComparisonAsync(bulletinId, characteristicId, valueId);

        var updateDto = new BulletinCharacteristicComparisonUpdateDto
        {
            CharacteristicValueId = Guid.NewGuid() // Несуществующее значение
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/BulletinCharacteristicComparison/{comparisonId}", updateDto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Positive()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();
        var bulletinId = await CreateTestBulletinAsync(userId, categoryId);
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var characteristicValueId = await CreateTestCharacteristicValueAsync(characteristicId);
        var comparisonId = await CreateTestCharacteristicComparisonAsync(bulletinId, characteristicId, characteristicValueId);

        // Act
        var response = await _client.DeleteAsync($"/api/BulletinCharacteristicComparison/{comparisonId}");
        var result = await response.Content.ReadFromJsonAsync<bool>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.True(result);

        // Проверяем что сопоставление действительно удалено
        var getResponse = await _client.GetAsync($"/api/BulletinCharacteristicComparison/{comparisonId}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_Negative()
    {
        // Arrange
        var unknownId = Guid.NewGuid();

        // Act
        var response = await _client.DeleteAsync($"/api/BulletinCharacteristicComparison/{unknownId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Create_DuplicateCharacteristicForSameBulletin_Negative()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();
        var bulletinId = await CreateTestBulletinAsync(userId, categoryId);
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var value1Id = await CreateTestCharacteristicValueAsync(characteristicId, "Значение один");
        var value2Id = await CreateTestCharacteristicValueAsync(characteristicId, "Значение два");

        // Создаем первое сопоставление
        await CreateTestCharacteristicComparisonAsync(bulletinId, characteristicId, value1Id);

        // Пытаемся создать второе сопоставление с той же характеристикой для того же объявления
        var duplicateDto = new BulletinCharacteristicComparisonCreateDto
        {
            BulletinId = bulletinId,
            CharacteristicId = characteristicId, // Та же характеристика
            CharacteristicValueId = value2Id    // Другое значение
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristicComparison", duplicateDto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Create_SameCharacteristicDifferentBulletins_Positive()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();

        // Создаем два объявления
        var bulletin1Id = await CreateTestBulletinAsync(userId, categoryId);
        var bulletin2Id = await CreateTestBulletinAsync(userId, categoryId);

        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var valueId = await CreateTestCharacteristicValueAsync(characteristicId);

        // Создаем первое сопоставление
        await CreateTestCharacteristicComparisonAsync(bulletin1Id, characteristicId, valueId);

        // Создаем второе сопоставление с той же характеристикой для другого объявления
        var secondDto = new BulletinCharacteristicComparisonCreateDto
        {
            BulletinId = bulletin2Id,           // Другое объявление
            CharacteristicId = characteristicId, // Та же характеристика
            CharacteristicValueId = valueId      // То же значение
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristicComparison", secondDto);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Create_CharacteristicFromDifferentCategory_Negative()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var category1Id = await CreateTestCategoryAsync("Категория один");
        var category2Id = await CreateTestCategoryAsync("Категория два");

        var bulletinId = await CreateTestBulletinAsync(userId, category1Id); // Объявление в категории 1
        var characteristicId = await CreateTestCharacteristicAsync(category2Id); // Характеристика из категории 2
        var valueId = await CreateTestCharacteristicValueAsync(characteristicId);

        var createDto = new BulletinCharacteristicComparisonCreateDto
        {
            BulletinId = bulletinId,
            CharacteristicId = characteristicId, // Характеристика из другой категории
            CharacteristicValueId = valueId
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristicComparison", createDto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}