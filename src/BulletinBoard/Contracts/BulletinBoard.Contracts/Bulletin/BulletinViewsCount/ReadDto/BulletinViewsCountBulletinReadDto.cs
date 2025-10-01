using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinViewsCount.ReadDto;

/// <summary>
/// Базовый формат данных количества просмотров.
/// Используется при чтении информации объявления.
/// </summary>
public class BulletinViewsCountBulletinReadDto
{
    /// <summary>
    /// Количество просмотров объявления
    /// </summary>
    public int ViewsCount { get; set; }
}
