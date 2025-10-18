using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.User.Services.IServices;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Contracts.Bulletin.BulletinUser.CreateDto;
using BulletinBoard.Contracts.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BulletinBoard.Hosts.BulletinAPI.Controllers;


/// <summary>
/// Контроллер работы с пользователями 
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
public class BulletinUserController : ControllerBase
{
    private readonly IBulletinUserService _userService;

    public BulletinUserController(IBulletinUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Создать пользователя.
    /// </summary>
    /// <param name="createDto">данные пользователя.</param>
    /// <param name="cancellationToken">токен отмены.</param>
    /// <returns>данные пользователя.</returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser(BulletinUserCreateDto createDto, CancellationToken cancellationToken)
    {
        BulletinUserDto userDto = await _userService.CreateAsync(createDto, cancellationToken);
        return Ok(userDto);
    }
}
