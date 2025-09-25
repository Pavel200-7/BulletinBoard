using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount;


namespace BulletinBoard.Contracts.Bulletin.Agrigates.Belletin;

/// <summary>
/// Предназначен для операции создания объявления со всеми его сопутствующими связями (дополняющими сущностями).
/// </summary>
public class BelletinCreateDto
{
    /// <summary>
    /// Основная сущность объявления
    /// </summary>
    public BulletinMainCreateDto Main { get; set; }

    /// <summary>
    /// Характеристики объявления
    /// </summary>
    public List<BulletinCharacteristicComparisonCreateDto> CharacteristicComparisons { get; set; }

    /// <summary>
    ///  Счетчик просмотров
    /// </summary>
    public BulletinViewsCountCreateDto ViewsCount { get; set; }

    /// <summary>
    /// Изображения объявления.
    /// </summary>
    public List<BulletinImageCreateDto> Images {  get; set; }
}
