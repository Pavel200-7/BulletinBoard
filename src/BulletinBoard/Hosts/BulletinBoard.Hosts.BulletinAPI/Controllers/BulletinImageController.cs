using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Contracts.Bulletin.BulletinUser.CreateDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.BulletinAPI.Controllers;

/// <summary>
/// Контроллер для работы с изображениями.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BulletinImageController : ControllerBase
{
    private readonly IBulletinImageService _userService;

    public BulletinImageController(IBulletinImageService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Создать изображение.
    /// </summary>
    /// <param name="createDto">данные изображения.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>данные изображения.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateImage(BulletinImageCreateDto createDto, CancellationToken cancellationToken)
    {
        BulletinImageDto result = await _userService.CreateAsync(createDto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удалить изображение.
    /// </summary>
    /// <param name="id">Id изображения.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImage(Guid id, CancellationToken cancellationToken)
    {
        bool result = await _userService.DeleteAsync(id, cancellationToken);
        return Ok(result);
    }



}
