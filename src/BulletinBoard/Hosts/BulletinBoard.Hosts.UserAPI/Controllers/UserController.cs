using BulletinBoard.AppServices.Contexts.User.Services.IServices;
using BulletinBoard.Contracts.Errors;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using BulletinBoard.Contracts.User.AuthDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace BulletinBoard.Hosts.UserAPI.Controllers;

/// <summary>
/// Контроллер аутентификации.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
public class AuthController : ControllerBase
{
    private IAuthService _authService;
    private ILogger<AuthController> _logger;

    /// <inheritdoc/>
    public AuthController
        (
        IAuthService authService,
        ILogger<AuthController> logger
        )
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Зарегистрировать пользователя.
    /// </summary>
    /// <param name="createDto">данные для создания аккаунта.</param>
    /// <returns>результат операции.</returns>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(ApplicationUserCreateDto createDto)
    {
        var token = await _authService.Register(createDto);
        return Ok();
    }

    /// <summary>
    /// Войти в систему.
    /// </summary>
    /// <param name="logInDto">Данные входа в систему.</param>
    /// <returns>токен доступа.</returns>
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LogIn(LogInDto logInDto)
    {
        var token = await _authService.LogIn(logInDto);
        return Ok(token);
    }

    /// <summary>
    /// Подтвердить почту.
    /// </summary>
    /// <param name="userId">id пользователя</param>
    /// <param name="token">токен подтверждения.</param>
    /// <returns>результат операции.</returns>
    [HttpGet]
    [AllowAnonymous]
    [Route("confirm_email")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        await _authService.ConfirmMailAsync(userId, token);
        return Ok();    
    }

    /// <summary>
    /// отправить новое сообщение для подтверждения почты.
    /// </summary>
    /// <returns>результат операции.</returns>
    [HttpGet]
    [Authorize]
    [Route("send_confirmation_email")]
    public async Task<IActionResult> SendNewConfirmationEmailAsync()
    {
        var sidClaim = User.FindFirst(ClaimTypes.Sid).Value;
        await _authService.SendNewConfirmationEmailAsync(sidClaim);
        return Ok();
    }
}
