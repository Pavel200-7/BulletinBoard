using BulletinBoard.Domain.Entities.User;
using BulletinBoard.Domain.Entities.User.Enums;
using BulletinBoard.Infrastructure.DataAccess.Contexts.User;
using DnsClient.Internal;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.Json;


namespace BulletinBoard.Infrastructure.ComponentRegistrar.Registrar.User;

/// <summary>
/// Проводит миграциб БД
/// </summary>
public class DbInitializer : IAsyncInitializer
{
    private readonly UserContext _userContext;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<DbInitializer> _logger;

    public DbInitializer(
        UserContext userContext,
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        ILogger<DbInitializer> logger
        )
    {
        _userContext = userContext;
        _roleManager = roleManager;
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
    }

    /// <summary>
    /// Проводит миграциб БД
    /// </summary>
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await _userContext.Database.MigrateAsync(cancellationToken);
        await InitializeRoles();
        await InitializeAdmin();
    }

    /// <summary>
    /// Добавить в таблицу ролей недостающие роли.
    /// </summary>
    /// <returns></returns>
    private async Task InitializeRoles()
    {
        string[] roles = typeof(Roles)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(string))
            .Select(f => (string)f.GetValue(null)!)
            .ToArray();

        foreach (string role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    /// <summary>
    /// Создать администратора.
    /// </summary>
    /// <returns></returns>
    private async Task InitializeAdmin()
    {
        const string adminRole = Roles.Admin; 

        var usersInAdminRole = await _userManager.GetUsersInRoleAsync(adminRole);
        if (usersInAdminRole.Count == 0)
        {
            var adminUser = new ApplicationUser();
            adminUser.UserName = _configuration["DefaultAdmin:UserName"];
            adminUser.Email = _configuration["DefaultAdmin:Email"];
            adminUser.EmailConfirmed = true;

            string adminPassword = _configuration["DefaultAdmin:Password"];

            var result = await _userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, adminRole);
                _logger.LogInformation("Администратор был создан.");
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError($"Ошибки создания администратора: {errors}");
            }
        }
    }
}