using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Repository;

/// <summary>
/// Класс для использования транзакций.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Начать транзакцию.
    /// </summary>
    Task BeginTransactionAsync();

    /// <summary>
    /// Зафиксировать транзакцию.
    /// </summary>
    Task CommitTransactionAsync();

    /// <summary>
    /// Откатить транзакцию
    /// </summary>
    Task RollbackTransactionAsync();
}
