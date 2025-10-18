using BulletinBoard.AppServices.Contexts.User.Repository;
using BulletinBoard.AppServices.Contexts.User.Services.IServices;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Contracts.User.ApplicationUserDto;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using BulletinBoard.Contracts.User.AuthDto;
using BulletinBoard.Domain.Entities.User.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace BulletinBoard.AppServices.Contexts.User.Services;

/// <inheritdoc/>
public class AuthService : IAuthService
{
    private IConfiguration _configuration;
    private IUserService _userService;
    private IUserEmailConformationService _emailConformationService;
    private ILogger<AuthService> _logger;

    /// <inheritdoc/>
    public AuthService
        (
        IConfiguration configuration,
        IUserService userService,
        IUserEmailConformationService emailConformationService,
        ILogger<AuthService> logger
        )
    {
        _configuration = configuration;
        _userService = userService;
        _emailConformationService = emailConformationService;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<string> Register(ApplicationUserCreateDto createDto)
    {
        string userId = await _userService.CreateAsync(createDto);

        await _userService.AddRoleAsync(userId, Roles.User);

        ApplicationUserDto userDto = await _userService.GetByIdAsync(userId);
        await _emailConformationService.SendNewConfirmationEmailAsync(userDto);

        return userId;
    }

    /// <inheritdoc/>
    public async Task<TokenDto> LogIn(LogInDto logInDto)
    {
        ApplicationUserDto? userDto = await _userService.GetByEmailAsync(logInDto.Email);

        _logger.LogInformation($"Получена сущность пользователя {JsonSerializer.Serialize(userDto)}.");

        if (userDto is null)
        {
            _logger.LogInformation($"Пользователь не найден.");
            throw new ValidationExeption(GetEmailValidationExeption()); 
        }

        
        if (!await _userService.CheckPassword(logInDto))
        {
            _logger.LogInformation($"Пароль не верен.");
            throw new ValidationExeption(GetPasswordValidationExeption());
        }

        TokenDto tokenDto = new TokenDto(GenerateJWTString(userDto));
        _logger.LogInformation($"Был создан токен jwt {JsonSerializer.Serialize(tokenDto)}.");

        return tokenDto;
    }

    /// <inheritdoc/>
    private string GenerateJWTString(ApplicationUserDto userDto)
    {
        List<Claim> claims = new();
        claims.Add(new Claim(ClaimTypes.Sid, userDto.Id));
        claims.Add(new Claim(ClaimTypes.Email, userDto.Email));
        userDto.Roles?.ForEach(role =>
            claims.Add(new Claim(ClaimTypes.Role, role)));


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
            );

        _logger.LogInformation($"Был создан токен с содержимым {JsonSerializer.Serialize(token)} до перевода в строку.");

        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }

    private IDictionary<string, string[]> GetEmailValidationExeption()
        => new Dictionary<string, string[]>() { { "Email", new string[] { "User with this email does not exist" } } };

    private IDictionary<string, string[]> GetPasswordValidationExeption()
        => new Dictionary<string, string[]>() { { "Password", new string[] { "Password is not valid" } } };

    /// <inheritdoc/>
    public async Task<bool> ConfirmMailAsync(string userId, string token)
    {
        return await _emailConformationService.ConfirmMailAsync(userId, token);
    }

    /// <inheritdoc/>
    public async Task<bool> SendNewConfirmationEmailAsync(string userId)
    {
        _logger.LogInformation($"Получен идентификатор {userId}.");

        ApplicationUserDto userDto = await _userService.GetByIdAsync(userId);
        _logger.LogInformation($"Получена сущность пользователя {JsonSerializer.Serialize(userDto)}.");
        await _emailConformationService.SendNewConfirmationEmailAsync(userDto);

        return true;
    }
}
