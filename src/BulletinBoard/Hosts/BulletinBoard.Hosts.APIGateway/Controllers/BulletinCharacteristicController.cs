using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.UpdateDto;
using BulletinBoard.Contracts.Errors;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Hosts.APIGateway.Controllers.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BulletinBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с характеристиками.
/// </summary>
[ApiController]
[Route("api/characteristic")]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class BulletinCharacteristicGatewayController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <inheritdoc/>
    public BulletinCharacteristicGatewayController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Получение характеристики по id.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    GET /api/characteristic/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="id">Идентификатор характеристики.</param>
    /// <returns>Базовый формат данных характеристики.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BulletinCharacteristicDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCharacteristic(Guid id)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.GetAsync($"/api/BulletinCharacteristic/{id}");
        return await response.ToActionResult();
    }

    /// <summary>
    /// Создание характеристики.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST /api/characteristic
    ///     {
    ///        "name": "Характеристика 1",
    ///        "categoryId": "01995805-ca8c-73c2-a120-4315a88208e8"
    ///     }
    ///
    /// </remarks>
    /// <param name="characteristic">Формат данных создания характеристики.</param>
    /// <returns>Базовый формат данных характеристики.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BulletinCharacteristicDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCharacteristic([FromBody] BulletinCharacteristicCreateDto characteristic)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.PostAsJsonAsync("/api/BulletinCharacteristic", characteristic);
        return await response.ToActionResult();
    }

    /// <summary>
    /// Обновить характеристику.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     PUT /api/characteristic/01995805-ca8c-73c2-a120-4315a88208e8
    ///     {
    ///        "name": "Характеристика 1",
    ///     }
    ///
    /// </remarks>
    /// <param name="id">id характеристики.</param>
    /// <param name="characteristic">Формат данных обновления характеристики.</param>
    /// <returns>Базовый формат данных характеристики.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BulletinCharacteristicDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCharacteristic(Guid id, [FromBody] BulletinCharacteristicUpdateDto characteristic)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.PutAsJsonAsync($"/api/BulletinCharacteristic/{id}", characteristic);
        return await response.ToActionResult();
    }

    /// <summary>
    /// Удалить характеристику.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     DELETE /api/characteristic/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="id">Идентификатор характеристики.</param>
    /// <returns>Результат удаления.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCharacteristic(Guid id)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.DeleteAsync($"/api/BulletinCharacteristic/{id}");
        return await response.ToActionResult();
    }

    /// <summary>
    /// Получение характеристики по id категории.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    GET /api/characteristic/bycategory/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="category_id">Идентификатор связанной категории.</param>
    /// <returns>Базовый формат данных характеристики.</returns>
    [HttpGet("byCategory/{category_id}")]
    [ProducesResponseType(typeof(IReadOnlyCollection<BulletinCharacteristic>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCategoryId(Guid category_id)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.GetAsync($"/api/BulletinCharacteristic/byCategory/{category_id}");
        return await response.ToActionResult();
    }
}