using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.UpdateDto;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace BulletinBoard.IntegrationTests.Hosts.Controllers;

public class BulletinCharacteristicControllerTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;
    private readonly IServiceScope _scope;
    private readonly BulletinContext _dbContext;

    public BulletinCharacteristicControllerTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
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
        return createdCharacteristic!.Id;
    }

    [Fact]
    public async Task GetCharacteristic_Positive()
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.GetAsync($"/api/BulletinCharacteristic/{characteristicId}");
        var receivedCharacteristic = await response.Content.ReadFromJsonAsync<BulletinCharacteristicDto>();
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("Тестовая характеристика", receivedCharacteristic?.Name);
        Assert.Equal(categoryId, receivedCharacteristic?.CategoryId);
    }

    [Fact]
    public async Task GetCharacteristic_Negative()
    {
        // Arrange
        var unknownId = Guid.NewGuid();

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.GetAsync($"/api/BulletinCharacteristic/{unknownId}");
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateCharacteristic_Positive()
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        var createDto = new BulletinCharacteristicCreateDto
        {
            Name = "Новая характеристика",
            CategoryId = categoryId
        };

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristic", createDto);
        var createdCharacteristic = await response.Content.ReadFromJsonAsync<BulletinCharacteristicDto>();
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(createDto.Name, createdCharacteristic?.Name);
        Assert.Equal(createDto.CategoryId, createdCharacteristic?.CategoryId);
        Assert.NotEqual(Guid.Empty, createdCharacteristic?.Id);
    }

    [Theory]
    [MemberData(nameof(CreateCharacteristic_Negative_DataSource))]
    public async Task CreateCharacteristic_Negative(BulletinCharacteristicCreateDto createDto)
    {
        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristic", createDto);
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public static IEnumerable<object[]> CreateCharacteristic_Negative_DataSource()
    {
        // Короткое название
        yield return new object[]
        {
            new BulletinCharacteristicCreateDto
            {
                Name = "Те",
                CategoryId = Guid.NewGuid()
            }
        };

        // Слишком длинное название
        yield return new object[]
        {
            new BulletinCharacteristicCreateDto
            {
                Name = new string('A', 36), // 36 символов > 35
                CategoryId = Guid.NewGuid()
            }
        };

        // Запрещенные символы
        yield return new object[]
        {
            new BulletinCharacteristicCreateDto
            {
                Name = "Характеристика.",
                CategoryId = Guid.NewGuid()
            }
        };

        // Несуществующая категория
        yield return new object[]
        {
            new BulletinCharacteristicCreateDto
            {
                Name = "Валидное название",
                CategoryId = Guid.NewGuid() // Несуществующий ID
            }
        };

        // Пустое название
        yield return new object[]
        {
            new BulletinCharacteristicCreateDto
            {
                Name = "",
                CategoryId = Guid.NewGuid()
            }
        };
    }

    [Fact]
    public async Task UpdateCharacteristic_Positive()
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);

        var updateDto = new BulletinCharacteristicUpdateDto
        {
            Name = "Обновленная характеристика"
        };

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.PutAsJsonAsync($"/api/BulletinCharacteristic/{characteristicId}", updateDto);
        var updatedCharacteristic = await response.Content.ReadFromJsonAsync<BulletinCharacteristicDto>();
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(updateDto.Name, updatedCharacteristic?.Name);
        Assert.Equal(categoryId, updatedCharacteristic?.CategoryId); // CategoryId не должен меняться
    }

    [Theory]
    [MemberData(nameof(UpdateCharacteristic_Negative_DataSource))]
    public async Task UpdateCharacteristic_Negative(BulletinCharacteristicUpdateDto updateDto, HttpStatusCode expectedStatusCode)
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.PutAsJsonAsync($"/api/BulletinCharacteristic/{characteristicId}", updateDto);
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    public static IEnumerable<object[]> UpdateCharacteristic_Negative_DataSource()
    {
        // Короткое название
        yield return new object[]
        {
            new BulletinCharacteristicUpdateDto { Name = "Те" },
            HttpStatusCode.BadRequest
        };

        // Слишком длинное название
        yield return new object[]
        {
            new BulletinCharacteristicUpdateDto { Name = new string('A', 36) },
            HttpStatusCode.BadRequest
        };

        // Запрещенные символы
        yield return new object[]
        {
            new BulletinCharacteristicUpdateDto { Name = "Название." },
            HttpStatusCode.BadRequest
        };

        // Пустое название
        yield return new object[]
        {
            new BulletinCharacteristicUpdateDto { Name = "" },
            HttpStatusCode.BadRequest
        };
    }

    [Fact]
    public async Task UpdateCharacteristic_NotFound()
    {
        // Arrange
        var updateDto = new BulletinCharacteristicUpdateDto { Name = "Обновленное название" };
        var unknownId = Guid.NewGuid();

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.PutAsJsonAsync($"/api/BulletinCharacteristic/{unknownId}", updateDto);
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCharacteristic_Positive()
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.DeleteAsync($"/api/BulletinCharacteristic/{characteristicId}");
        var result = await response.Content.ReadFromJsonAsync<bool>();
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.True(result);

        // Проверяем что характеристика действительно удалена
        var getResponse = await _client.GetAsync($"/api/BulletinCharacteristic/{characteristicId}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteCharacteristic_Negative()
    {
        // Arrange
        var unknownId = Guid.NewGuid();

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.DeleteAsync($"/api/BulletinCharacteristic/{unknownId}");
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateCharacteristic_DuplicateNameInSameCategory_Negative()
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        await CreateTestCharacteristicAsync(categoryId, "Уникальная характеристика");

        var duplicateDto = new BulletinCharacteristicCreateDto
        {
            Name = "Уникальная характеристика", // То же самое имя
            CategoryId = categoryId
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristic", duplicateDto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateCharacteristic_SameNameDifferentCategories_Positive()
    {
        // Arrange
        var category1Id = await CreateTestCategoryAsync("Категория один");
        var category2Id = await CreateTestCategoryAsync("Категория два");

        await CreateTestCharacteristicAsync(category1Id, "Общее название");

        var characteristicDto = new BulletinCharacteristicCreateDto
        {
            Name = "Общее название", // То же имя, но другая категория
            CategoryId = category2Id
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristic", characteristicDto);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateCharacteristic_NonLeafyCategory_Negative()
    {
        // Arrange
        var nonLeafyCategoryId = await CreateTestCategoryAsync("Не листовая категория", false);

        var createDto = new BulletinCharacteristicCreateDto
        {
            Name = "Характеристика для не листовой",
            CategoryId = nonLeafyCategoryId
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristic", createDto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}