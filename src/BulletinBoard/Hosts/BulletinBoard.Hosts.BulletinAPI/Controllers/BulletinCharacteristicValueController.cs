using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
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
[Route("api/[controller]")]
[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
public class BulletinCharacteristicValueController : ControllerBase
{
    private readonly IBulletinCharacteristicValueService _characteristicValueService;

    /// <inheritdoc/>
    public BulletinCharacteristicValueController(IBulletinCharacteristicValueService service)
    {
        _characteristicValueService = service;
    }

    /// <summary>
    /// Получить значение характеристики по id.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    GET api/BulletinCharacteristicValue/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="id">Идентификатор значения характеристики.</param>
    /// <returns>Базовый формат данных значения характеристики.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BulletinCharacteristicValueDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var dto = await _characteristicValueService.GetByIdAsync(id);
        return Ok(dto);
    }

    /// <summary>
    /// Создать новое значение характеристики.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    Post api/BulletinCharacteristicValue
    ///    {
    ///         "characteristicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///         "value": "Значени хорактеристики"
    ///     }
    ///
    /// </remarks>
    /// <param name="createDto">Формат данных создания характеристики.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Формат данных значения характеристики.</returns>
    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(typeof(BulletinCharacteristicValueDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] BulletinCharacteristicValueCreateDto createDto, CancellationToken cancellationToken)
    {
        var created = await _characteristicValueService.CreateAsync(createDto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновить значение характеристики по id.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    Put api/BulletinCharacteristicValue
    ///    {
    ///         "value": "Значени хорактеристики"
    ///    }
    ///
    /// </remarks>
    /// <param name="id">id значения характеристики.</param>
    /// <param name="updateDto">Формат данных обновления значения характеристики.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Формат данных значения характеристики.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BulletinCharacteristicValueDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] BulletinCharacteristicValueUpdateDto updateDto, CancellationToken cancellationToken)
    {
        var updated = await _characteristicValueService.UpdateAsync(id, updateDto, cancellationToken);
        return Ok(updated);
    }

    /// <summary>
    /// Удалить значение характеристики по id.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    Delete api/BulletinCharacteristicValue/3fa85f64-5717-4562-b3fc-2c963f66afa6
    ///
    /// </remarks>
    /// <param name="id">id значения характеристики.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат удаления.</returns>
    [HttpDelete("{id}")]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _characteristicValueService.DeleteAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получить все значения по id характеристики.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    Get api/BulletinCharacteristicValue/byCharacteristic/3fa85f64-5717-4562-b3fc-2c963f66afa6
    ///
    /// </remarks>
    /// <param name="characteristicId">id характеристики.</param>
    /// <returns>Колкекция - результат отбора.</returns>
    [HttpGet("byCharacteristic/{characteristicId}")]
    [ProducesResponseType(typeof(IReadOnlyCollection<BulletinCharacteristicValueDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCharacteristic(Guid characteristicId)
    {
        var result = await _characteristicValueService.GetByCharacteristicAsync(characteristicId);
        return Ok(result);
    }
}