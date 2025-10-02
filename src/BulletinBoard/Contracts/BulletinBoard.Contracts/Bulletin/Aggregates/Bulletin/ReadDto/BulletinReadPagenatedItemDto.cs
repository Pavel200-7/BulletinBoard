using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.ReadDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Contracts.Bulletin.BulletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinMain.ReadDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.ReadDto;

/// <summary>
/// Элемент пагинации объявлений.
/// Он упрощен по сравнению с обычной ДТО.
/// </summary>
public class BulletinReadPagenatedItemDto
{
    /// <summary>
    /// Основная сущность объявления
    /// </summary>
    public BulletinMainBulletinReadDto Main { get; set; }

    /// <summary>
    ///  Счетчик просмотров
    /// </summary>
    public int ViewsCount { get; set; }

    /// <summary>
    /// Рейтинги
    /// </summary>
    public decimal Rating { get; set; }

    /// <summary>
    /// Главное изображение объявления.
    /// </summary>
    public BulletinImageDto? MainImage { get; set; }
}
