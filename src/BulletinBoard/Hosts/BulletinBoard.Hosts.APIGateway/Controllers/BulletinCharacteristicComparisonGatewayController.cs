using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.UpdateDto;
using BulletinBoard.Contracts.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с сопоставлениями характеристик и объявлений.
/// </summary>
[ApiController]
[Route("api/characteristic-comparison")]
[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
public class BulletinCharacteristicComparisonGatewayController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <inheritdoc/>
    public BulletinCharacteristicComparisonGatewayController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Получение сопоставления характеристики с объявлением по id.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    GET /api/characteristic-comparison/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="id">Идентификатор сопоставления.</param>
    /// <returns>Базовый формат данных сопоставления.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCharacteristicComparison(Guid id)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.GetAsync($"/api/BulletinCharacteristicComparison/{id}");
        var content = await response.Content.ReadAsStringAsync();
        return Content(content, "application/json");
    }

    /// <summary>
    /// Создание сопоставления характеристики с объявлением.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST /api/characteristic-comparison
    ///     {
    ///        "bulletinId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///        "characteristicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///        "characteristicValueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    ///     }
    ///
    /// </remarks>
    /// <param name="comparison">Формат данных создания сопоставления.</param>
    /// <returns>Базовый формат данных сопоставления.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCharacteristicComparison([FromBody] BulletinCharacteristicComparisonCreateDto comparison)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.PostAsJsonAsync("/api/BulletinCharacteristicComparison", comparison);
        var content = await response.Content.ReadAsStringAsync();
        return Content(content, "application/json");
    }

    /// <summary>
    /// Изменение сопоставления характеристики с объявлением.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     PUT /api/characteristic-comparison/01992a7c-71f5-7081-9311-44884bac8d58
    ///     {
    ///        "characteristicValueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    ///     }
    ///
    /// </remarks>
    /// <param name="id">Идентификатор сопоставления, которое нужно обновить.</param>
    /// <param name="comparison">Формат данных изменения сопоставления.</param>
    /// <returns>Базовый формат данных сопоставления.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCharacteristicComparison(Guid id, [FromBody] BulletinCharacteristicComparisonUpdateDto comparison)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.PutAsJsonAsync($"/api/BulletinCharacteristicComparison/{id}", comparison);
        var content = await response.Content.ReadAsStringAsync();
        return Content(content, "application/json");
    }

    /// <summary>
    /// Удалить сопоставление характеристики с объявлением.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     DELETE /api/characteristic-comparison/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="id">Идентификатор сопоставления, которое нужно удалить.</param>
    /// <returns>true если все прошло успешно.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCharacteristicComparison(Guid id)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        var response = await client.DeleteAsync($"/api/BulletinCharacteristicComparison/{id}");
        var content = await response.Content.ReadAsStringAsync();
        return Content(content, "application/json");
    }
}