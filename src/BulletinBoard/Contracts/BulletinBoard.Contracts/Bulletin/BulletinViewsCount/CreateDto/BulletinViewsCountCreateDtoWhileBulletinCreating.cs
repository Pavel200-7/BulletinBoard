using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinViewsCount.CreateDto;

/// <summary>
/// Формат данных создания количества просмотров.
/// Используется, при создании объявления, когда его id еще нет.
/// </summary>
public class BulletinViewsCountCreateDtoWhileBulletinCreating
{
    /// <summary>
    /// Количество просмотров объявления
    /// </summary>
    public int ViewsCount { get; set; }
}
