using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.UpdateDto;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Xunit.Abstractions;

namespace BulletinBoard.IntegrationTests.Hosts.Controllers;

public class BulletinCharacteristicValueControllerTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;
    private readonly IServiceScope _scope;
    private readonly BulletinContext _dbContext;

    public BulletinCharacteristicValueControllerTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
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

    [Fact]
    public async Task GetById_Positive()
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var valueId = await CreateTestCharacteristicValueAsync(characteristicId);

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.GetAsync($"/api/BulletinCharacteristicValue/{valueId}");
        var receivedValue = await response.Content.ReadFromJsonAsync<BulletinCharacteristicValueDto>();
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("Тестовое значение", receivedValue.Value);
        Assert.Equal(characteristicId, receivedValue.CharacteristicId);
    }

    [Fact]
    public async Task GetById_Negative()
    {
        // Arrange
        var unknownId = Guid.NewGuid();

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.GetAsync($"/api/BulletinCharacteristicValue/{unknownId}");
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Create_Positive()
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);

        var createDto = new BulletinCharacteristicValueCreateDto
        {
            CharacteristicId = characteristicId,
            Value = "Новое значение характеристики"
        };

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristicValue", createDto);
        var createdValue = await response.Content.ReadFromJsonAsync<BulletinCharacteristicValueDto>();
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(createDto.Value, createdValue.Value);
        Assert.Equal(createDto.CharacteristicId, createdValue.CharacteristicId);
        Assert.NotEqual(Guid.Empty, createdValue.Id);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(Create_Negative_DataSource))]
    public async Task Create_Negative(BulletinCharacteristicValueCreateDto createDto)
    {
        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristicValue", createDto);
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public static IEnumerable<object[]> Create_Negative_DataSource()
    {
        // Короткое значение
        yield return new object[]
        {
            new BulletinCharacteristicValueCreateDto
            {
                CharacteristicId = Guid.NewGuid(),
                Value = "Те"
            }
        };

        // Слишком длинное значение
        yield return new object[]
        {
            new BulletinCharacteristicValueCreateDto
            {
                CharacteristicId = Guid.NewGuid(),
                Value = new string('A', 36) // 36 символов > 35
            }
        };

        // Запрещенные символы
        yield return new object[]
        {
            new BulletinCharacteristicValueCreateDto
            {
                CharacteristicId = Guid.NewGuid(),
                Value = "Значение."
            }
        };

        // Пустое значение
        yield return new object[]
        {
            new BulletinCharacteristicValueCreateDto
            {
                CharacteristicId = Guid.NewGuid(),
                Value = ""
            }
        };

        // Несуществующая характеристика
        yield return new object[]
        {
            new BulletinCharacteristicValueCreateDto
            {
                CharacteristicId = Guid.NewGuid(), // Несуществующий ID
                Value = "Валидное значение"
            }
        };
    }

    [Fact]
    public async Task Update_Positive()
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var valueId = await CreateTestCharacteristicValueAsync(characteristicId);

        var updateDto = new BulletinCharacteristicValueUpdateDto
        {
            Value = "Обновленное значение"
        };

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.PutAsJsonAsync($"/api/BulletinCharacteristicValue/{valueId}", updateDto);
        var updatedValue = await response.Content.ReadFromJsonAsync<BulletinCharacteristicValueDto>();
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(updateDto.Value, updatedValue.Value);
        Assert.Equal(characteristicId, updatedValue.CharacteristicId); // CharacteristicId не должен меняться
    }

    [Theory]
    [MemberData(nameof(Update_Negative_DataSource))]
    public async Task Update_Negative(BulletinCharacteristicValueUpdateDto updateDto, HttpStatusCode expectedStatusCode)
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var valueId = await CreateTestCharacteristicValueAsync(characteristicId);

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.PutAsJsonAsync($"/api/BulletinCharacteristicValue/{valueId}", updateDto);
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    public static IEnumerable<object[]> Update_Negative_DataSource()
    {
        // Короткое значение
        yield return new object[]
        {
            new BulletinCharacteristicValueUpdateDto { Value = "Те" },
            HttpStatusCode.BadRequest
        };

        // Слишком длинное значение
        yield return new object[]
        {
            new BulletinCharacteristicValueUpdateDto { Value = new string('A', 36) },
            HttpStatusCode.BadRequest
        };

        // Запрещенные символы
        yield return new object[]
        {
            new BulletinCharacteristicValueUpdateDto { Value = "Значение." },
            HttpStatusCode.BadRequest
        };

        // Пустое значение
        yield return new object[]
        {
            new BulletinCharacteristicValueUpdateDto { Value = "" },
            HttpStatusCode.BadRequest
        };
    }

    [Fact]
    public async Task Update_NotFound()
    {
        // Arrange
        var updateDto = new BulletinCharacteristicValueUpdateDto { Value = "Обновленное значение" };
        var unknownId = Guid.NewGuid();

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.PutAsJsonAsync($"/api/BulletinCharacteristicValue/{unknownId}", updateDto);
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Positive()
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        var valueId = await CreateTestCharacteristicValueAsync(characteristicId);

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.DeleteAsync($"/api/BulletinCharacteristicValue/{valueId}");
        var result = await response.Content.ReadFromJsonAsync<bool>();
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.True(result);

        // Проверяем что значение действительно удалено
        var getResponse = await _client.GetAsync($"/api/BulletinCharacteristicValue/{valueId}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_Negative()
    {
        // Arrange
        var unknownId = Guid.NewGuid();

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.DeleteAsync($"/api/BulletinCharacteristicValue/{unknownId}");
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetByCharacteristic_Positive()
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);

        // Создаем несколько значений для одной характеристики
        await CreateTestCharacteristicValueAsync(characteristicId, "Значение 1");
        await CreateTestCharacteristicValueAsync(characteristicId, "Значение 2");
        await CreateTestCharacteristicValueAsync(characteristicId, "Значение 3");

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.GetAsync($"/api/BulletinCharacteristicValue/byCharacteristic/{characteristicId}");
        var values = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<BulletinCharacteristicValueDto>>();
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(values);
        Assert.Equal(3, values.Count);
        Assert.All(values, v => Assert.Equal(characteristicId, v.CharacteristicId));
        Assert.Contains(values, v => v.Value == "Значение 1");
        Assert.Contains(values, v => v.Value == "Значение 2");
        Assert.Contains(values, v => v.Value == "Значение 3");
    }

    [Fact]
    public async Task GetByCharacteristic_EmptyResult()
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        // Не создаем значений для этой характеристики

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.GetAsync($"/api/BulletinCharacteristicValue/byCharacteristic/{characteristicId}");
        var values = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<BulletinCharacteristicValueDto>>();
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(values);
        Assert.Empty(values);
    }

    [Fact]
    public async Task GetByCharacteristic_NonExistentCharacteristic()
    {
        // Arrange
        var unknownCharacteristicId = Guid.NewGuid();

        // Act
        var stopwatch = Stopwatch.StartNew();
        var response = await _client.GetAsync($"/api/BulletinCharacteristicValue/byCharacteristic/{unknownCharacteristicId}");
        var values = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<BulletinCharacteristicValueDto>>();
        stopwatch.Stop();

        _output.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(values);
        Assert.Empty(values); // Для несуществующей характеристики возвращается пустой список
    }

    [Fact]
    public async Task Create_DuplicateValueInSameCharacteristic_Negative()
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);
        await CreateTestCharacteristicValueAsync(characteristicId, "Уникальное значение");

        var duplicateDto = new BulletinCharacteristicValueCreateDto
        {
            CharacteristicId = characteristicId,
            Value = "Уникальное значение" // То же самое значение
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristicValue", duplicateDto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Create_SameValueDifferentCharacteristics_Positive()
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        var characteristic1Id = await CreateTestCharacteristicAsync(categoryId, "Характеристика 1");
        var characteristic2Id = await CreateTestCharacteristicAsync(categoryId, "Характеристика 2");

        await CreateTestCharacteristicValueAsync(characteristic1Id, "Общее значение");

        var valueDto = new BulletinCharacteristicValueCreateDto
        {
            CharacteristicId = characteristic2Id,
            Value = "Общее значение" // То же значение, но другая характеристика
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/BulletinCharacteristicValue", valueDto);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Update_DuplicateValueInSameCharacteristic_Negative()
    {
        // Arrange
        var categoryId = await CreateTestCategoryAsync();
        var characteristicId = await CreateTestCharacteristicAsync(categoryId);

        // Создаем два разных значения
        var value1Id = await CreateTestCharacteristicValueAsync(characteristicId, "Значение 1");
        await CreateTestCharacteristicValueAsync(characteristicId, "Значение 2");

        // Пытаемся изменить первое значение на "Значение 2" (дубликат)
        var updateDto = new BulletinCharacteristicValueUpdateDto
        {
            Value = "Значение 2"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/BulletinCharacteristicValue/{value1Id}", updateDto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}