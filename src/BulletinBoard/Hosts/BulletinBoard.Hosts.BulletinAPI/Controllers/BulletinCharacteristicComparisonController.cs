using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.UpdateDto;
using BulletinBoard.Contracts.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с сопоставлениями характеристик и объявлений.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
public class BulletinCharacteristicComparisonController : ControllerBase
{
    private readonly IBulletinCharacteristicComparisonService _characteristicComparisonService;

    /// <inheritdoc/>
    public BulletinCharacteristicComparisonController(IBulletinCharacteristicComparisonService characteristicComparisonService)
    {
        _characteristicComparisonService = characteristicComparisonService;
    }

    /// <summary>
    /// Получение сопоставления характеристики с объявлением по id.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///    GET api/BulletinCharacteristicComparison/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="id">Идентификатор сопоставления.</param>
    /// <returns>Базовый формат данных сопоставления.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BulletinCharacteristicComparisonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var dto = await _characteristicComparisonService.GetByIdAsync(id);
        return Ok(dto);
    }

    /// <summary>
    /// Создание сопоставления характеристики с объявлением.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST api/BulletinCharacteristicComparison
    ///     {
    ///        "bulletinId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///        "characteristicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///        "characteristicValueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    ///     }
    ///
    /// </remarks>
    /// <param name="createDto">Формат данных создания сопоставления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных сопоставления.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BulletinCharacteristicComparisonDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] BulletinCharacteristicComparisonCreateDto createDto, CancellationToken cancellationToken)
    {
        var created = await _characteristicComparisonService.CreateAsync(createDto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Изменение сопоставления характеристики с объявлением.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     PUT api/BulletinCharacteristicComparison/01992a7c-71f5-7081-9311-44884bac8d58
    ///     {
    ///        "characteristicValueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    ///     }
    ///
    /// </remarks>
    /// <param name="id">Идентификатор сопоставления, которое нужно обновить.</param>
    /// <param name="updateDto">Формат данных изменения сопоставления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных сопоставления.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BulletinCharacteristicComparisonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] BulletinCharacteristicComparisonUpdateDto updateDto, CancellationToken cancellationToken)
    {
        var updated = await _characteristicComparisonService.UpdateAsync(id, updateDto, cancellationToken);
        return Ok(updated);
    }

    /// <summary>
    /// Удалить сопоставление характеристики с объявлением.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     DELETE api/BulletinCharacteristicComparison/01992529-1ec8-766f-a03d-a7ac4f0996b9
    ///
    /// </remarks>
    /// <param name="id">Идентификатор сопоставления, которое нужно удалить.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>true если все прошло успешно.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _characteristicComparisonService.DeleteAsync(id, cancellationToken);
        return Ok(result);
    }
}