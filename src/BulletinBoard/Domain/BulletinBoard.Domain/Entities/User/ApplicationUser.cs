using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Domain.Entities.User;

/// <summary>
/// Пользователь
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Широта (местоположение)
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Долгота (местоположение)
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Адрес (наименование местоположения)
    /// </summary>
    public string? FormattedAddress { get; set; }

    /// <summary>
    /// Список ролей
    /// </summary>
    public virtual List<IdentityRole> Roles { get; set; }
}
