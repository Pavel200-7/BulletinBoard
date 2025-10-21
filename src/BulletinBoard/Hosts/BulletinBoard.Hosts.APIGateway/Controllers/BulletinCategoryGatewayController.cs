using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.ReadDto;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.UpdateDto;
using BulletinBoard.Contracts.Errors;
using BulletinBoard.Hosts.APIGateway.Controllers.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using System.Text.Json;

namespace BulletinBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с категориями объявлений.
/// </summary>
[ApiController]
[Route("api/category")]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class BulletinCategoryGatewayController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <inheritdoc/>
    public BulletinCategoryGatewayController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Получение категории по id.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    GET /api/category/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="id">Идентификатор категории.</param>
    /// <returns>Базовый формат данных категории.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BulletinCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.GetAsync($"/api/BulletinCategory/{id}");
        return await response.ToActionResult();
    }

    /// <summary>
    /// Создание категории.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST /api/category
    ///     {
    ///        "parentCategoryId" : "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///        "categoryName" : "Категория 1",
    ///        "isLeafy": false
    ///     }
    ///
    /// </remarks>
    /// <param name="category">Формат данных создания категории.</param>
    /// <returns>Базовый формат данных категории.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BulletinCategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCategory([FromBody] BulletinCategoryCreateDto category)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.PostAsJsonAsync("/api/BulletinCategory", category);
        return await response.ToActionResult();
    }

    /// <summary>
    /// Изменение категории.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     PUT /api/category/01992a7c-71f5-7081-9311-44884bac8d58
    ///     {
    ///        "parentCategoryId" : "0199252f-2d8c-7892-ada3-91600c780739",
    ///        "categoryName" : "Категория 5",
    ///     }
    ///
    /// </remarks>
    /// <param name="id">Идентификатор категории, которую нужно обновить.</param>
    /// <param name="category">Формат данных изменения категории.</param>
    /// <returns>Базовый формат данных категории.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BulletinCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] BulletinCategoryUpdateDto category)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.PutAsJsonAsync($"/api/BulletinCategory/{id}", category);
        return await response.ToActionResult();
    }

    /// <summary>
    /// Удалить категорию.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     DELETE /api/category/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="id">Идентификатор категории, которую нужно удалить.</param>
    /// <returns>true если все прошло успешно.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.DeleteAsync($"/api/BulletinCategory/{id}");
        return await response.ToActionResult();
    }

    /// <summary>
    /// Получение всех категорий в их правильном иерархическом виде.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     GET /api/category
    ///
    /// </remarks>
    /// <returns>Формат данных для вывода всех категорий в их правильном иерархическом виде.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(BulletinCategoryReadAllDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCategories()
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.GetAsync("/api/BulletinCategory");
        return await response.ToActionResult();
    }

    /// <summary>
    /// Получение одной категории в виде древовидной струкруры от самого корня.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     GET /api/category/01992529-1ec8-766f-a03d-a7ac4f0996b9/singlechain
    ///
    /// </remarks>
    /// <returns>Формат данных для вывода одной карегории в виде древовидной струкруры от самого корня.</returns>
    [HttpGet("{id}/singlechain")]
    [ProducesResponseType(typeof(BulletinCategoryReadAllDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategorySingleChain(Guid id)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.GetAsync($"/api/BulletinCategory/{id}/SingleChain");
        return await response.ToActionResult();
    }
}