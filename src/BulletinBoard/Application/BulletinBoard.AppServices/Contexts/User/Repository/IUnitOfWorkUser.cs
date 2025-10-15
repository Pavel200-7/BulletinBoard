using BulletinBoard.AppServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.User.Repository;

/// <summary>
/// Класс для работы с транзакциями и репозиториями домена User.
/// </summary>
public interface IUnitOfWorkUser : IUnitOfWork
{
}
