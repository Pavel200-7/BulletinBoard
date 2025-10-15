using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.User.Repository;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.User;

/// <summary>
/// Компонент, реализующий паттерн unit of work.
/// Из него возможно запускать транзакции.
/// Он предоставляет доступ ко всем репозиториям текущего домена.
/// </summary>
public class UnitOfWorkUser : BaseUnitOfWork<UserContext>, IUnitOfWorkUser
{
    public UnitOfWorkUser(UserContext context) : base(context)
    {
    }
}