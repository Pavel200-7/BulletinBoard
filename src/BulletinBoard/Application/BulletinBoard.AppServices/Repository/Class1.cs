using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// TODO : это будет декоратор для транзакций.
namespace BulletinBoard.AppServices.Repository;

/// <summary>
/// Класс для обертки выполнения транзакций.
/// </summary>
public interface ITransactionExecutor
{

    
    /// <summary>
    /// Выполнить операцию транз
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="action"></param>
    /// <returns></returns>
    Task<T> ExecuteAsync<T>(Func<Task<T>> action);

    Task ExecuteAsync(Func<Task> action);
}
