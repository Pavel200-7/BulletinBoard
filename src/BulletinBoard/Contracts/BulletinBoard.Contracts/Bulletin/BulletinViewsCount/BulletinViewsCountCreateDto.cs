using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinViewsCount;

/// <summary>
/// Формат данных создания количества просмотров 
/// </summary>
public class BulletinViewsCountCreateDto
{
    /// <summary>
    /// Id объявления
    /// </summary>
    public Guid BulletinId { get; set; }

    /// <summary>
    /// Количество просмотров объявления
    /// </summary>
    public int ViewsCount { get; set; }
}
