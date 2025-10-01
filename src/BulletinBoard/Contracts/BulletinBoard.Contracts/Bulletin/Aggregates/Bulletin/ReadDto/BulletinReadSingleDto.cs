using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.ReadDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Contracts.Bulletin.BulletinImage.ReadDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Contracts.Bulletin.BulletinRating.ReadDto;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.ReadDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.ReadDto;

/// <summary>
/// Базовый формат объявления (как совокупности связанных сущностей)
/// Данные характеристик включают вместо id сопоставляемые значения.
/// </summary>
public class BulletinReadSingleDto : BulletinDto
{
    /// <summary>
    /// Характеристики объявления (их значения)
    /// </summary>
    public new List<BulletinCharacteristicComparisonBulletinReadDto> CharacteristicComparisons { get; set; }

    /// <summary>
    ///  Счетчик просмотров
    /// </summary>
    public new BulletinViewsCountBulletinReadDto ViewsCount { get; set; }

    /// <summary>
    /// Рейтинги
    /// </summary>
    public new  List<BulletinRatingBulletinReadDto> Ratings { get; set; }

    /// <summary>
    /// Изображения объявления.
    /// </summary>
    public new List<BulletinImageBulletinReadDto> Images { get; set; }
}
