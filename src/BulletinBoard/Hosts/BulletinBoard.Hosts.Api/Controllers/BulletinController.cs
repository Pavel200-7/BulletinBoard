using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.Contracts.Bulletin.Agrigates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы с объявлениями.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
public class BulletinController : ControllerBase
{
    private readonly IBulletinMainService _bulletinMainService;
    private readonly IBulletinService _bulletinService;



    /// <inheritdoc/>
    public BulletinController
        (
        IBulletinMainService bulletinMainService,
        IBulletinService bulletinService
        )
    {
        _bulletinMainService = bulletinMainService;
        _bulletinService = bulletinService;
    }

    /// <summary>
    /// Создание объявления.
    /// </summary>
    /// <param name="bulletinCreateDtoAPIVersion">Формат данных создания нового объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных категории.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBulletin(BulletinCreateDtoRequest bulletinCreateDtoAPIVersion, CancellationToken cancellationToken)
    {
        List<BulletinImageCreateDtoWhileBulletinCreating> imagesCreateDto = new()
        {
            new BulletinImageCreateDtoWhileBulletinCreating() { Id = Guid.NewGuid(), IsMain = true },
            new BulletinImageCreateDtoWhileBulletinCreating() { Id = Guid.NewGuid(), IsMain = false },
            new BulletinImageCreateDtoWhileBulletinCreating() { Id = Guid.NewGuid(), IsMain = false },
            new BulletinImageCreateDtoWhileBulletinCreating() { Id = Guid.NewGuid(), IsMain = false },
            new BulletinImageCreateDtoWhileBulletinCreating() { Id = Guid.NewGuid(), IsMain = false },
        };

        BulletinCreateDtoController bulletinCreateDto = new BulletinCreateDtoController
        {
            BulletinMain = bulletinCreateDtoAPIVersion.BulletinMain,
            CharacteristicComparisons = bulletinCreateDtoAPIVersion.CharacteristicComparisons,
            Images = imagesCreateDto
        };

        Guid bulletinId = await _bulletinService.CreateAsync(bulletinCreateDto, cancellationToken);
        return Ok(bulletinId);
    }
}
