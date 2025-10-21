using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.UpdateDto;
using BulletinBoard.Contracts.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с характеристиками.
/// </summary>
[ApiController]
[Authorize]
[Authorize(Policy = "RequireAdminRole")]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
public class BulletinCharacteristicController : ControllerBase
{
    private readonly IBulletinCharacteristicService _bulletinCharacteristicService;

    /// <inheritdoc/>
    public BulletinCharacteristicController(IBulletinCharacteristicService bulletinCharacteristicService)
    {
        _bulletinCharacteristicService = bulletinCharacteristicService;
    }

    /// <summary>
    /// Получение характеристики по id.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    GET api/BulletinCharacteristic/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="id">Идентификатор характеристики.</param>
    /// <returns>Базовый формат данных характеристики.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BulletinCharacteristicDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCharacteristic(Guid id)
    {
        var categoryDto = await _bulletinCharacteristicService.GetByIdAsync(id);
        return Ok(categoryDto);
    }

    /// <summary>
    /// Создание характеристики.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST api/BulletinCharacteristic
    ///     {
    ///        "name": "Характеристика 1",
    ///        "categoryId": "01995805-ca8c-73c2-a120-4315a88208e8"
    ///     }
    ///     
    ///
    /// </remarks>
    /// <param name="characteristic">Формат данных создания характеристики.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных характеристики.</returns>
    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(typeof(BulletinCharacteristicDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCharacteristic(BulletinCharacteristicCreateDto characteristic, CancellationToken cancellationToken)
    {
        var createdCharacteristic = await _bulletinCharacteristicService.CreateAsync(characteristic, cancellationToken);
        return Ok(createdCharacteristic);
    }

    /// <summary>
    /// Обновить характеристику.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     Put api/BulletinCharacteristic/01995805-ca8c-73c2-a120-4315a88208e8
    ///     {
    ///        "name": "Характеристика 1",
    ///     }
    ///     
    /// </remarks>
    /// <param name="id">id характеристики.</param>
    /// <param name="characteristic">Формат данных обновления характеристики.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных характеристики.</returns>
    [HttpPut("{id}")]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(typeof(BulletinCharacteristicDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCharacteristic(Guid id, BulletinCharacteristicUpdateDto characteristic, CancellationToken cancellationToken)
    {
        var updetedCharacteristic = await _bulletinCharacteristicService.UpdateAsync(id, characteristic, cancellationToken);
        return Ok(updetedCharacteristic);
    }

    /// <summary>
    /// Удалить характеристику.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///         DELETE api/BulletinCharacteristic/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="id">Идентификатор характеристики.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат удаления.</returns>
    [HttpDelete("{id}")]
    [Authorize(Policy = "RequireAdminRole")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCharacteristic(Guid id, CancellationToken cancellationToken)
    {
        var result = await _bulletinCharacteristicService.DeleteAsync(id, cancellationToken);
        return Ok(result);
    }
}