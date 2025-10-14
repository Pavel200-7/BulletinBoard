using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.EntityFrameworkCore;


namespace BulletinBoard.Infrastructure.ComponentRegistrar.Registrar.Bulletin;

/// <summary>
/// Проводит миграциб БД
/// </summary>
public class DbInitializer : IAsyncInitializer
{
    private readonly BulletinContext _bulletinContext;

    public DbInitializer(BulletinContext bulletinContext)
    {
        _bulletinContext = bulletinContext;
    }

    /// <summary>
    /// Проводит миграциб БД
    /// </summary>
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await _bulletinContext.Database.MigrateAsync(cancellationToken);
    }
}