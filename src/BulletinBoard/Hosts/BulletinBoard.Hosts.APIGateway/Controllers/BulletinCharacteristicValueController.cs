using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.UpdateDto;
using BulletinBoard.Contracts.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с возможными значениями характеристик.
/// </summary>
[ApiController]
[Route("api/characteristic-value")]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
public class BulletinCharacteristicValueGatewayController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <inheritdoc/>
    public BulletinCharacteristicValueGatewayController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Получить значение характеристики по id.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    GET /api/characteristic-value/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="id">Идентификатор значения характеристики.</param>
    /// <returns>Базовый формат данных значения характеристики.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCharacteristicValue(Guid id)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.GetAsync($"/api/BulletinCharacteristicValue/{id}");
        var content = await response.Content.ReadAsStringAsync();
        return StatusCode((int)response.StatusCode, content);
    }

    /// <summary>
    /// Создать новое значение характеристики.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    POST /api/characteristic-value
    ///    {
    ///         "characteristicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///         "value": "Значени хорактеристики"
    ///     }
    ///
    /// </remarks>
    /// <param name="characteristicValue">Формат данных создания характеристики.</param>
    /// <returns>Формат данных значения характеристики.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCharacteristicValue([FromBody] BulletinCharacteristicValueCreateDto characteristicValue)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.PostAsJsonAsync("/api/BulletinCharacteristicValue", characteristicValue);
        var content = await response.Content.ReadAsStringAsync();
        return StatusCode((int)response.StatusCode, content);
    }

    /// <summary>
    /// Обновить значение характеристики по id.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    PUT /api/characteristic-value/3fa85f64-5717-4562-b3fc-2c963f66afa6
    ///    {
    ///         "value": "Значени хорактеристики"
    ///    }
    ///
    /// </remarks>
    /// <param name="id">id значения характеристики.</param>
    /// <param name="characteristicValue">Формат данных обновления значения характеристики.</param>
    /// <returns>Формат данных значения характеристики.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCharacteristicValue(Guid id, [FromBody] BulletinCharacteristicValueUpdateDto characteristicValue)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.PutAsJsonAsync($"/api/BulletinCharacteristicValue/{id}", characteristicValue);
        var content = await response.Content.ReadAsStringAsync();
        return StatusCode((int)response.StatusCode, content);
    }

    /// <summary>
    /// Удалить значение характеристики по id.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    DELETE /api/characteristic-value/3fa85f64-5717-4562-b3fc-2c963f66afa6
    ///
    /// </remarks>
    /// <param name="id">id значения характеристики.</param>
    /// <returns>Результат удаления.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCharacteristicValue(Guid id)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.DeleteAsync($"/api/BulletinCharacteristicValue/{id}");
        var content = await response.Content.ReadAsStringAsync();
        return StatusCode((int)response.StatusCode, content);
    }

    /// <summary>
    /// Получить все значения по id характеристики.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    GET /api/characteristic-value/byCharacteristic/3fa85f64-5717-4562-b3fc-2c963f66afa6
    ///
    /// </remarks>
    /// <param name="characteristicId">id характеристики.</param>
    /// <returns>Колкекция - результат отбора.</returns>
    [HttpGet("byCharacteristic/{characteristicId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCharacteristicValuesByCharacteristic(Guid characteristicId)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.GetAsync($"/api/BulletinCharacteristicValue/byCharacteristic/{characteristicId}");
        var content = await response.Content.ReadAsStringAsync();
        return StatusCode((int)response.StatusCode, content);
    }
}