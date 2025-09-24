using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinViewsCount;

/// <summary>
/// Формат данных создания количества просмотров 
/// </summary>
public class BulletinViewsCountUpdateDto
{
    /// <summary>
    /// Количество просмотров объявления
    /// </summary>
    public int ViewsCount { get; set; }
}
