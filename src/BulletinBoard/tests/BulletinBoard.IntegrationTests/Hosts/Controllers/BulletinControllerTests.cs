using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.ReadDto;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.UpdateDto;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace BulletinBoard.IntegrationTests.Hosts.Controllers;

public class BulletinControllerTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;
    private readonly IServiceScope _scope;
    private readonly BulletinContext _dbContext;

    public BulletinControllerTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
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
        // Очищаем в правильном порядке из-за foreign keys
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
        return createdCategory!.Id;
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
        return createdValue!.Id;
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

    private BulletinCreateDtoRequest CreateTestBulletinCreateDto(Guid userId, Guid categoryId, List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating> characteristics)
    {
        return new BulletinCreateDtoRequest
        {
            BulletinMain = new BulletinMainCreateDto
            {
                UserId = userId,
                Title = "Тестовое объявление",
                Description = "Это тестовое описание объявления",
                CategoryId = categoryId,
                Price = 1000.50m
            },
            CharacteristicComparisons = characteristics
        };
    }

    private async Task AddTestRatingAsync(Guid bulletinId, Guid userId, int rating = 5, string comment = "Отличный товар")
    {
        var newRating = new BulletinRating()
        {
            Id = Guid.NewGuid(),
            BulletinId = bulletinId,
            UserId = userId,
            Rating = rating,
            CreatedAt = DateTime.UtcNow
        };
        await _dbContext.BulletinRating.AddAsync(newRating);
        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task GetById_Positive()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var characteristicValueId = await CreateTestCharacteristicValueAsync(characteristicId);
        
        var characteristics = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>
        {
            new() { CharacteristicId = characteristicId, CharacteristicValueId = characteristicValueId }
        };

        var createDto = CreateTestBulletinCreateDto(userId, categoryId, characteristics);
        var createResponse = await _client.PostAsJsonAsync("/api/Bulletin", createDto);
        createResponse.EnsureSuccessStatusCode();
        var bulletinId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        // Act
        var response = await _client.GetAsync($"/api/Bulletin/{bulletinId}");
        var bulletin = await response.Content.ReadFromJsonAsync<BulletinDto>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(bulletin);
        Assert.Equal(bulletinId, bulletin.Main.Id);
        Assert.Equal("Тестовое объявление", bulletin.Main.Title);
        Assert.Equal("Это тестовое описание объявления", bulletin.Main.Description);
        Assert.Equal(1000.50m, bulletin.Main.Price);
    }

    [Fact]
    public async Task GetById_Negative()
    {
        // Arrange
        var unknownId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/Bulletin/{unknownId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Create_Positive()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var characteristicValueId = await CreateTestCharacteristicValueAsync(characteristicId);
        
        var characteristics = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>
        {
            new() { CharacteristicId = characteristicId, CharacteristicValueId = characteristicValueId }
        };

        var createDto = CreateTestBulletinCreateDto(userId, categoryId, characteristics);

        // Act
        var response = await _client.PostAsJsonAsync("/api/Bulletin", createDto);
        var bulletinId = await response.Content.ReadFromJsonAsync<Guid>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotEqual(Guid.Empty, bulletinId);

        // Проверяем что объявление действительно создано
        var getResponse = await _client.GetAsync($"/api/Bulletin/{bulletinId}");
        getResponse.EnsureSuccessStatusCode();
    }

    [Theory]
    [MemberData(nameof(Create_Negative_DataSource))]
    public async Task Create_Negative(BulletinCreateDtoRequest createDto)
    {
        // Act
        var response = await _client.PostAsJsonAsync("/api/Bulletin", createDto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public static IEnumerable<object[]> Create_Negative_DataSource()
    {
        // Пустой заголовок
        yield return new object[]
        {
            new BulletinCreateDtoRequest
            {
                BulletinMain = new BulletinMainCreateDto
                {
                    UserId = Guid.NewGuid(),
                    Title = "",
                    Description = "Валидное описание",
                    CategoryId = Guid.NewGuid(),
                    Price = 1000m
                },
                CharacteristicComparisons = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>()
            }
        };

        // Слишком короткий заголовок
        yield return new object[]
        {
            new BulletinCreateDtoRequest
            {
                BulletinMain = new BulletinMainCreateDto
                {
                    UserId = Guid.NewGuid(),
                    Title = "Те",
                    Description = "Валидное описание",
                    CategoryId = Guid.NewGuid(),
                    Price = 1000m
                },
                CharacteristicComparisons = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>()
            }
        };

        // Отрицательная цена
        yield return new object[]
        {
            new BulletinCreateDtoRequest
            {
                BulletinMain = new BulletinMainCreateDto
                {
                    UserId = Guid.NewGuid(),
                    Title = "Валидный заголовок",
                    Description = "Валидное описание",
                    CategoryId = Guid.NewGuid(),
                    Price = -100m
                },
                CharacteristicComparisons = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>()
            }
        };

        // Пустое описание
        yield return new object[]
        {
            new BulletinCreateDtoRequest
            {
                BulletinMain = new BulletinMainCreateDto
                {
                    UserId = Guid.NewGuid(),
                    Title = "Валидный заголовок",
                    Description = "",
                    CategoryId = Guid.NewGuid(),
                    Price = 1000m
                },
                CharacteristicComparisons = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>()
            }
        };

        // Несуществующая категория
        yield return new object[]
        {
            new BulletinCreateDtoRequest
            {
                BulletinMain = new BulletinMainCreateDto
                {
                    UserId = Guid.NewGuid(),
                    Title = "Валидный заголовок",
                    Description = "Валидное описание",
                    CategoryId = Guid.NewGuid(), // Несуществующий ID
                    Price = 1000m
                },
                CharacteristicComparisons = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>()
            }
        };
    }

    [Fact]
    public async Task Update_Positive()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var characteristicValueId = await CreateTestCharacteristicValueAsync(characteristicId);
        
        var characteristics = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>
        {
            new() { CharacteristicId = characteristicId, CharacteristicValueId = characteristicValueId }
        };

        var createDto = CreateTestBulletinCreateDto(userId, categoryId, characteristics);
        var createResponse = await _client.PostAsJsonAsync("/api/Bulletin", createDto);
        var bulletinId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var updateDto = new BulletinMainUpdateDto
        {
            Title = "Обновленный заголовок",
            Description = "Обновленное описание",
            Price = 2000.75m
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/Bulletin?id={bulletinId}", updateDto);
        var updatedBulletinId = await response.Content.ReadFromJsonAsync<Guid>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(bulletinId, updatedBulletinId);

        // Проверяем что объявление действительно обновлено
        var getResponse = await _client.GetAsync($"/api/Bulletin/{bulletinId}");
        var bulletin = await getResponse.Content.ReadFromJsonAsync<BulletinDto>();
        Assert.Equal("Обновленный заголовок", bulletin?.Main.Title);
        Assert.Equal("Обновленное описание", bulletin?.Main.Description);
        Assert.Equal(2000.75m, bulletin?.Main.Price);
    }

    [Theory]
    [MemberData(nameof(Update_Negative_DataSource))]
    public async Task Update_Negative(BulletinMainUpdateDto updateDto)
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var characteristicValueId = await CreateTestCharacteristicValueAsync(characteristicId);
        
        var characteristics = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>
        {
            new() { CharacteristicId = characteristicId, CharacteristicValueId = characteristicValueId }
        };

        var createDto = CreateTestBulletinCreateDto(userId, categoryId, characteristics);
        var createResponse = await _client.PostAsJsonAsync("/api/Bulletin", createDto);
        var bulletinId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        // Act
        var response = await _client.PutAsJsonAsync($"/api/Bulletin?id={bulletinId}", updateDto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public static IEnumerable<object[]> Update_Negative_DataSource()
    {
        // Пустой заголовок
        yield return new object[]
        {
            new BulletinMainUpdateDto
            {
                Title = "",
                Description = "Валидное описание",
                Price = 1000m
            }
        };

        // Слишком короткий заголовок
        yield return new object[]
        {
            new BulletinMainUpdateDto
            {
                Title = "Те",
                Description = "Валидное описание",
                Price = 1000m
            }
        };

        // Отрицательная цена
        yield return new object[]
        {
            new BulletinMainUpdateDto
            {
                Title = "Валидный заголовок",
                Description = "Валидное описание",
                Price = -100m
            }
        };

        // Пустое описание
        yield return new object[]
        {
            new BulletinMainUpdateDto
            {
                Title = "Валидный заголовок",
                Description = "",
                Price = 1000m
            }
        };
    }

    [Fact]
    public async Task Update_NotFound()
    {
        // Arrange
        var updateDto = new BulletinMainUpdateDto
        {
            Title = "Обновленный заголовок",
            Description = "Обновленное описание",
            Price = 2000m
        };
        var unknownId = Guid.NewGuid();

        // Act
        var response = await _client.PutAsJsonAsync($"/api/Bulletin?id={unknownId}", updateDto);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Positive()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var characteristicValueId = await CreateTestCharacteristicValueAsync(characteristicId);
        
        var characteristics = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>
        {
            new() { CharacteristicId = characteristicId, CharacteristicValueId = characteristicValueId }
        };

        var createDto = CreateTestBulletinCreateDto(userId, categoryId, characteristics);
        var createResponse = await _client.PostAsJsonAsync("/api/Bulletin", createDto);
        var bulletinId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        // Act
        var response = await _client.DeleteAsync($"/api/Bulletin?id={bulletinId}");
        var result = await response.Content.ReadFromJsonAsync<bool>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.True(result);

        // Проверяем что объявление действительно удалено
        var getResponse = await _client.GetAsync($"/api/Bulletin/{bulletinId}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_Negative()
    {
        // Arrange
        var unknownId = Guid.NewGuid();

        // Act
        var response = await _client.DeleteAsync($"/api/Bulletin?id={unknownId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Create_WithMultipleCharacteristics_Positive()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();
        
        // Создаем несколько характеристик для категории
        var characteristic1Id = await CreateTestCharacteristicAsync(categoryId, "Цвет");
        var characteristic2Id = await CreateTestCharacteristicAsync(categoryId, "Размер");
        var characteristic3Id = await CreateTestCharacteristicAsync(categoryId, "Материал");
        
        var value1Id = await CreateTestCharacteristicValueAsync(characteristic1Id, "Красный");
        var value2Id = await CreateTestCharacteristicValueAsync(characteristic2Id, "Большой");
        var value3Id = await CreateTestCharacteristicValueAsync(characteristic3Id, "Дерево");

        var characteristics = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>
        {
            new() { CharacteristicId = characteristic1Id, CharacteristicValueId = value1Id },
            new() { CharacteristicId = characteristic2Id, CharacteristicValueId = value2Id },
            new() { CharacteristicId = characteristic3Id, CharacteristicValueId = value3Id }
        };

        var createDto = CreateTestBulletinCreateDto(userId, categoryId, characteristics);

        // Act
        var response = await _client.PostAsJsonAsync("/api/Bulletin", createDto);
        var bulletinId = await response.Content.ReadFromJsonAsync<Guid>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotEqual(Guid.Empty, bulletinId);

        // Проверяем что объявление создано со всеми характеристиками
        var getResponse = await _client.GetAsync($"/api/Bulletin/{bulletinId}");
        var bulletin = await getResponse.Content.ReadFromJsonAsync<BulletinDto>();
        Assert.NotNull(bulletin?.CharacteristicComparisons);
        Assert.Equal(3, bulletin.CharacteristicComparisons.Count);
    }

    [Fact]
    public async Task Create_WithNonLeafyCategory_Negative()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync("Родительская категория", false); // isLeafy = false
        
        var characteristics = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>();

        var createDto = CreateTestBulletinCreateDto(userId, categoryId, characteristics);

        // Act
        var response = await _client.PostAsJsonAsync("/api/Bulletin", createDto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Create_WithInvalidCharacteristic_Negative()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();
        
        // Создаем характеристику для ДРУГОЙ категории
        var otherCategoryId = await CreateTestCategoryAsync("Другая категория");
        var characteristicId = await CreateTestCharacteristicAsync(otherCategoryId);
        var characteristicValueId = await CreateTestCharacteristicValueAsync(characteristicId);

        var characteristics = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>
        {
            new() { CharacteristicId = characteristicId, CharacteristicValueId = characteristicValueId }
        };

        var createDto = CreateTestBulletinCreateDto(userId, categoryId, characteristics);

        // Act
        var response = await _client.PostAsJsonAsync("/api/Bulletin", createDto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetByIdReadSingle_Positive()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var characteristicValueId = await CreateTestCharacteristicValueAsync(characteristicId);

        var characteristics = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>
        {
            new() { CharacteristicId = characteristicId, CharacteristicValueId = characteristicValueId }
        };

        var createDto = CreateTestBulletinCreateDto(userId, categoryId, characteristics);
        var createResponse = await _client.PostAsJsonAsync("/api/Bulletin", createDto);
        createResponse.EnsureSuccessStatusCode();
        var bulletinId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        // Добавляем рейтинг для тестирования
        await AddTestRatingAsync(bulletinId, userId, 5, "Отличный товар");

        // Act
        var response = await _client.GetAsync($"/api/Bulletin/{bulletinId}/Single");
        var bulletin = await response.Content.ReadFromJsonAsync<BulletinReadSingleDto>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(bulletin);
        Assert.Equal(bulletinId, bulletin.Main.Id);
        Assert.Equal("Тестовое объявление", bulletin.Main.Title);
        Assert.Equal("Это тестовое описание объявления", bulletin.Main.Description);
        Assert.Equal(1000.50m, bulletin.Main.Price);

        Assert.NotNull(bulletin.CharacteristicComparisons);
        Assert.Single(bulletin.CharacteristicComparisons);
        Assert.Equal("Тестовая характеристика", bulletin.CharacteristicComparisons[0].Characteristic);
        Assert.Equal("Тестовое значение", bulletin.CharacteristicComparisons[0].CharacteristicValue);

        Assert.NotNull(bulletin.ViewsCount);
        Assert.Equal(0, bulletin.ViewsCount.ViewsCount); 

        Assert.NotNull(bulletin.Ratings);
        Assert.Single(bulletin.Ratings);
        Assert.Equal(5, bulletin.Ratings[0].Rating);
        Assert.Equal(userId, bulletin.Ratings[0].UserId);

        Assert.NotNull(bulletin.Images);
        Assert.Equal(5, bulletin.Images.Count); 
        Assert.Contains(bulletin.Images, img => img.IsMain);
    }

    [Fact]
    public async Task GetByIdReadSingle_WithMultipleCharacteristics_Positive()
    {
        // Arrange
        var userId = await CreateTestUserAsync();
        var categoryId = await CreateTestCategoryAsync();

        // Создаем несколько характеристик
        var characteristic1Id = await CreateTestCharacteristicAsync(categoryId, "Цвет");
        var characteristic2Id = await CreateTestCharacteristicAsync(categoryId, "Размер");

        var value1Id = await CreateTestCharacteristicValueAsync(characteristic1Id, "Красный");
        var value2Id = await CreateTestCharacteristicValueAsync(characteristic2Id, "Большой");

        var characteristics = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>
        {
            new() { CharacteristicId = characteristic1Id, CharacteristicValueId = value1Id },
            new() { CharacteristicId = characteristic2Id, CharacteristicValueId = value2Id }
        };

        var createDto = CreateTestBulletinCreateDto(userId, categoryId, characteristics);
        var createResponse = await _client.PostAsJsonAsync("/api/Bulletin", createDto);
        createResponse.EnsureSuccessStatusCode();
        var bulletinId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        // Act
        var response = await _client.GetAsync($"/api/Bulletin/{bulletinId}/Single");
        var bulletin = await response.Content.ReadFromJsonAsync<BulletinReadSingleDto>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(bulletin);
        Assert.NotNull(bulletin.CharacteristicComparisons);
        Assert.Equal(2, bulletin.CharacteristicComparisons.Count);


        var colorCharacteristic = bulletin.CharacteristicComparisons.FirstOrDefault(c => c.Characteristic == "Цвет");
        Assert.NotNull(colorCharacteristic);
        Assert.Equal("Красный", colorCharacteristic.CharacteristicValue);

        var sizeCharacteristic = bulletin.CharacteristicComparisons.FirstOrDefault(c => c.Characteristic == "Размер");
        Assert.NotNull(sizeCharacteristic);
        Assert.Equal("Большой", sizeCharacteristic.CharacteristicValue);
    }

    [Fact]
    public async Task GetByIdReadSingle_Negative()
    {
        // Arrange
        var unknownId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/Bulletin/{unknownId}/Single");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}