using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.CreateDto;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.IntegrationTests.Hosts.Controllers;

public class BulletinCategoryControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    public readonly CustomWebApplicationFactory _factory;
    public readonly HttpClient Client;


    public BulletinCategoryControllerTests
        (
        CustomWebApplicationFactory factory
        )
    {
        _factory = factory;
        Client = _factory.CreateClient();
    }

    [Fact]
    public async Task Post_CreateCategory()
    {
        // Arrange
        string url = "/api/BulletinCategory";
        BulletinCategoryCreateDto createDto = new()
        {
            ParentCategoryId = null,
            CategoryName = "Новая категория",
            IsLeafy = false
        }; 

        var json = JsonConvert.SerializeObject(createDto);
        var context = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync(url, context);


        response.EnsureSuccessStatusCode();
        var responceJson = await response.Content.ReadAsStringAsync();
        var createdCategory = JsonConvert.DeserializeObject<BulletinCategoryDto>(responceJson);

        // Assert
        Assert.Equal(createdCategory.CategoryName, createDto.CategoryName);
    }


    [Theory]
    [MemberData(nameof(CreateTestData))]
    public async Task Post_CreateCategory_ValidationError(BulletinCategoryCreateDto createDto, int expectedStatusCode)
    {
        // Arrange
        string url = "/api/BulletinCategory";

        var json = JsonConvert.SerializeObject(createDto);
        var context = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync(url, context);


        Assert.Equal(expectedStatusCode, (int)response.StatusCode);
    }

    public static IEnumerable<object[]> CreateTestData()
    {
        yield return new object[]
        {
        new BulletinCategoryCreateDto
        {
            ParentCategoryId = null,
            CategoryName = "К", // Слишком короткое
            IsLeafy = false
        },
        400
        };
        yield return new object[]
        {
        new BulletinCategoryCreateDto
        {
            ParentCategoryId = Guid.NewGuid(),
            CategoryName = "", // Пустое имя
            IsLeafy = true
        },
        400
        };
        yield return new object[]
        {
        new BulletinCategoryCreateDto
        {
            ParentCategoryId = null,
            CategoryName = "Нормальная категория", // Валидные данные
            IsLeafy = false
        },
        200
        };
    }
}
