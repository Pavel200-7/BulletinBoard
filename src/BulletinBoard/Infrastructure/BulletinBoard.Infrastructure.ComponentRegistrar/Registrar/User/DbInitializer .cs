using BulletinBoard.Domain.Entities.User;
using BulletinBoard.Domain.Entities.User.Enums;
using BulletinBoard.Infrastructure.DataAccess.Contexts.User;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace BulletinBoard.Infrastructure.ComponentRegistrar.Registrar.User;

/// <summary>
/// Проводит миграциб БД
/// </summary>
public class DbInitializer : IAsyncInitializer
{
    private readonly UserContext _userContext;
    private readonly RoleManager<IdentityRole> _roleManager;


    public DbInitializer
        (
        UserContext userContext,
        RoleManager<IdentityRole> roleManager
        )
    {
        _userContext = userContext;
        _roleManager = roleManager;
    }

    /// <summary>
    /// Проводит миграциб БД
    /// </summary>
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await _userContext.Database.MigrateAsync(cancellationToken);
        await InitializeRoles();
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
}