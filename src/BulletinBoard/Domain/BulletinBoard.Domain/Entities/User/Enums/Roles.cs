using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Domain.Entities.User.Enums;

/// <summary>
/// Возможные роли.
/// </summary>
public static class Roles
{
    /// <summary>
    /// Ажминистратор
    /// </summary>
    public const string Admin = "Admin";

    /// <summary>
    /// Пользователь
    /// </summary>
    public const string User = "User";
}
