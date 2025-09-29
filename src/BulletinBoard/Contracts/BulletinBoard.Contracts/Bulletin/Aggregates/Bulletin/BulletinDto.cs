using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Contracts.Bulletin.BulletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount;


namespace BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin;

/// <summary>
/// Базовый формат объявления (как совокупности связанных сущностей)
/// </summary>
public class BulletinDto
{
    /// <summary>
    /// Основная сущность объявления
    /// </summary>
    public BulletinMainDto Main { get; set; }

    /// <summary>
    /// Характеристики объявления
    /// </summary>
    public List<BulletinCharacteristicComparisonDto> CharacteristicComparisons { get; set; }

    /// <summary>
    ///  Счетчик просмотров
    /// </summary>
    public BulletinViewsCountDto ViewsCount { get; set; }

    /// <summary>
    /// Рейтинги
    /// </summary>
    public List<BulletinRatingDto> Ratings { get; set; }

    /// <summary>
    /// Изображения объявления.
    /// </summary>
    public List<BulletinImageDto> Images { get; set; }
}
