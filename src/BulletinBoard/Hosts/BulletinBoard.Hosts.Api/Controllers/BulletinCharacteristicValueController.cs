//using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
//using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
//using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.CreateDto;
//using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.FilterDto;
//using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.UpdateDto;
//using BulletinBoard.Contracts.Errors;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading;

//namespace BulletinBoard.Hosts.Api.Controllers;


///// <summary>
///// Контроллер для работы с возможными значениями характеристик.
///// </summary>
//[ApiController]
//[Route("api/[controller]")]
//[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
//public class BulletinCharacteristicValueController : ControllerBase
//{
//    private readonly IBulletinCharacteristicValueService _characteristicValueService;

//    /// <inheritdoc/>
//    public BulletinCharacteristicValueController(IBulletinCharacteristicValueService service)
//    {
//        _characteristicValueService = service;
//    }

//    /// <summary>
//    /// Получить значение характеристики по id.
//    /// </summary>
//    [HttpGet("{id}")]
//    [ProducesResponseType(typeof(BulletinCharacteristicValueDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> GetById(Guid id)
//    {
//        var dto = await _characteristicValueService.GetByIdAsync(id);
//        return Ok(dto);
//    }

//    /// <summary>
//    /// Получить список значений с фильтром.
//    /// </summary>
//    [HttpPost("filter")]
//    [ProducesResponseType(typeof(IReadOnlyCollection<BulletinCharacteristicValueDto>), StatusCodes.Status200OK)]
//    public async Task<IActionResult> GetFiltered([FromBody] BulletinCharacteristicValueFilterDto filterDto)
//    {
//        var result = await _characteristicValueService.GetAsync(filterDto);
//        return Ok(result);
//    }

//    /// <summary>
//    /// Создать новое значение характеристики.
//    /// </summary>
//    [HttpPost]
//    [ProducesResponseType(typeof(BulletinCharacteristicValueDto), StatusCodes.Status201Created)]
//    public async Task<IActionResult> Create([FromBody] BulletinCharacteristicValueCreateDto createDto, CancellationToken cancellationToken)
//    {
//        var created = await _characteristicValueService.CreateAsync(createDto, cancellationToken);
//        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
//    }

//    /// <summary>
//    /// Обновить значение характеристики по id.
//    /// </summary>
//    [HttpPut("{id}")]
//    [ProducesResponseType(typeof(BulletinCharacteristicValueDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> Update(Guid id, [FromBody] BulletinCharacteristicValueUpdateDto updateDto, CancellationToken cancellationToken)
//    {
//        var updated = await _characteristicValueService.UpdateAsync(id, updateDto, cancellationToken);
//        return Ok(updated);
//    }

//    /// <summary>
//    /// Удалить значение характеристики по id.
//    /// </summary>
//    [HttpDelete("{id}")]
//    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
//    {
//        var result = await _characteristicValueService.DeleteAsync(id, cancellationToken);
//        return Ok(result);
//    }

//    /// <summary>
//    /// Получить все значения по характеристике.
//    /// </summary>
//    [HttpGet("byCharacteristic/{characteristicId}")]
//    [ProducesResponseType(typeof(IReadOnlyCollection<BulletinCharacteristicValueDto>), StatusCodes.Status200OK)]
//    public async Task<IActionResult> GetByCharacteristic(Guid characteristicId)
//    {
//        var result = await _characteristicValueService.GetByCharacteristicAsync(characteristicId);
//        return Ok(result);
//    }
//}