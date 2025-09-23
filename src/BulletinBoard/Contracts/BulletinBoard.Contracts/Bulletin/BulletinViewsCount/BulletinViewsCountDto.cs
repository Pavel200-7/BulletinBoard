using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinViewsCount;

/// <summary>
/// Базовый формат данных количества просмотров 
/// </summary>
public class BulletinViewsCountDto
{
    /// <summary>
    /// Id количества просмотров 
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Id объявления
    /// </summary>
    public Guid BulletinId { get; set; }

    /// <summary>
    /// Количество просмотров объявления
    /// </summary>
    public int ViewsCount { get; set; }
}
