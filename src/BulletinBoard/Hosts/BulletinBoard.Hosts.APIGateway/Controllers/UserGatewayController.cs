using AutoMapper;
using BulletinBoard.AppServices.Contexts.Apigateway.Services.IServices;
using BulletinBoard.AppServices.Contexts.User.Services.IServices;
using BulletinBoard.Contracts.Bulletin.BulletinUser.CreateDto;
using BulletinBoard.Contracts.Errors;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using BulletinBoard.Contracts.User.AuthDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using System.Security.Claims;
using System.Text.Json;

namespace BulletinBoard.Hosts.APIGateway.Controllers;

/// <summary>
/// Контроллер аутентификации.
/// </summary>
[ApiController]
[Route("api/auth")]
[Authorize]
[ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
public class UserGatewayController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMapper _automapper;

    /// <inheritdoc/>
    public UserGatewayController
        (
        IHttpClientFactory httpClientFactory,
        IMapper automapper
        )
    {
        _httpClientFactory = httpClientFactory;
        _automapper = automapper;
    }

    /// <summary>
    /// Зарегистрировать пользователя.
    /// </summary>
    /// <param name="createDto">данные для создания аккаунта.</param>
    /// <returns>Id пользователя.</returns>
    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(ApplicationUserCreateDto createDto)
    {
        var client = _httpClientFactory.CreateClient("UserService");
        var response = await client.PostAsJsonAsync($"/api/auth/register", createDto);

        var userId = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            await CreateUserInBulletinDomain(userId, createDto);
        }

        var content = await response.Content.ReadAsStringAsync();
        return StatusCode((int)response.StatusCode, JsonSerializer.Deserialize<JsonElement>(content));
    }

    /// <summary>
    /// Добавляет пользователя в домен объявлений.
    /// </summary>
    /// <returns></returns>
    private async Task CreateUserInBulletinDomain(string userId, ApplicationUserCreateDto createDto)
    {
        var client = _httpClientFactory.CreateClient("BulletinService");
        BulletinUserCreateDto userCreateDto = _automapper.Map<BulletinUserCreateDto>(createDto);
        userCreateDto.Id = Guid.Parse(userId);

        var response = await client.PostAsJsonAsync($"/api/BulletinUser", userCreateDto);
    }

    /// <summary>
    /// Войти в систему.
    /// </summary>
    /// <param name="logInDto">Данные входа в систему.</param>
    /// <returns>токен доступа.</returns>
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> LogIn(LogInDto logInDto)
    {
        var client = _httpClientFactory.CreateClient("UserService");
        var response = await client.PostAsJsonAsync($"/api/auth/login", logInDto);
        var content = await response.Content.ReadAsStringAsync();

        return StatusCode((int)response.StatusCode, JsonSerializer.Deserialize<JsonElement>(content));
    }

    /// <summary>
    /// Подтвердить почту.
    /// </summary>
    /// <param name="userId">id пользователя</param>
    /// <param name="token">токен подтверждения.</param>
    /// <returns>результат операции.</returns>
    [HttpGet]
    [Route("confirm_email")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        var client = _httpClientFactory.CreateClient("UserService");
        var response = await client.GetAsync($"/api/auth/confirm_email?userId={userId}&token={Uri.EscapeDataString(token)}");

        if (response.IsSuccessStatusCode)
        {
            return Ok("Email confirmed successfully");
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, JsonSerializer.Deserialize<JsonElement>(errorContent));
        }
    }

    /// <summary>
    /// отправить новое сообщение для подтверждения почты.
    /// </summary>
    /// <returns>результат операции.</returns>
    [HttpGet]
    [Route("send_confirmation_email")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SendNewConfirmationEmailAsync()
    {
        var sidClaim = User.FindFirst(ClaimTypes.Sid)?.Value;

        if (string.IsNullOrEmpty(sidClaim))
        {
            return Unauthorized("User ID claim not found");
        }

        var client = _httpClientFactory.CreateClient("UserService");
        var response = await client.GetAsync($"/api/auth/send_confirmation_email");

        if (response.IsSuccessStatusCode)
        {
            return Ok("Confirmation email sent successfully");
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, JsonSerializer.Deserialize<JsonElement>(errorContent));
        }
    }
}