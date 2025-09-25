using BulletinBoard.Contracts.Bulletin.BelletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;


namespace BulletinBoard.Contracts.Bulletin.Agrigates.Bulletin.CreateDto;

/// <summary>
/// Предназначен для операции создания объявления со всеми его сопутствующими связями (дополняющими сущностями).
/// </summary>
public class BelletinCreateDto
{
    /// <summary>
    /// Основная сущность объявления
    /// </summary>
    public BulletinMainCreateDto BulletinMain { get; set; }

    /// <summary>
    /// Характеристики объявления
    /// </summary>
    public List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating> CharacteristicComparisons { get; set; }

    /// <summary>
    /// Изображения объявления.
    /// </summary>
    public List<BulletinImageCreateDtoWhileBulletinCreating> Images {  get; set; }
}
