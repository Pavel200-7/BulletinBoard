using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Api.Controllers;

/// <inheritdoc/>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
public class BulletinMainController : ControllerBase
{
    private readonly IBulletinMainService _bulletinService;

    /// <inheritdoc/>
    public BulletinMainController(IBulletinMainService bulletinService)
    {
        _bulletinService = bulletinService;
    }


    /// <summary>
    /// Создание нового объявления
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(BulletinMainDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(BulletinMainCreateDto createDto)
    {
        var createdBulletin = await _bulletinService.CreateAsync(createDto);
        return Ok(createdBulletin);
    }

    /// <summary>
    /// Обновление объявления по id
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BulletinMainDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, BulletinMainUpdateDto updateDto)
    {
        var updatedBulletin = await _bulletinService.UpdateAsync(id, updateDto);
        return Ok(updatedBulletin);
    }

    /// <summary>
    /// Удаление объявления по id
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool result = await _bulletinService.DeleteAsync(id);
        return Ok(result);
    }

   
}