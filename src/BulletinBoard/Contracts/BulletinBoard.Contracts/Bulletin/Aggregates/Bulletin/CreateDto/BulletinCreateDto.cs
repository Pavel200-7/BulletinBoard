using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.CreateDto;


namespace BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;

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

    /// <summary>
    /// Счетчик просмотров.
    /// </summary>
    public BulletinViewsCountCreateDtoWhileBulletinCreating ViewsCount { get; set; }

    /// <summary>
    /// Конструктор объявления.
    /// </summary>
    public BelletinCreateDto(BulletinMainCreateDto main)
    {
        BulletinMain = main;
        CharacteristicComparisons = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>();
        Images = new List<BulletinImageCreateDtoWhileBulletinCreating>();
    }

    /// <summary>
    /// Добавить характеристики.
    /// </summary>
    /// <param name="characteristicComparisons"></param>
    public void AddCharacteristicComparisons(List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating> characteristicComparisons)
    {
        if (characteristicComparisons != null && characteristicComparisons.Any())
        {
            CharacteristicComparisons.AddRange(characteristicComparisons);
        }
    }

    /// <summary>
    /// Добавить изображения.
    /// </summary>
    public void AddImages(List<BulletinImageCreateDtoWhileBulletinCreating>  images)
    {
        if (images != null && images.Any())
        {
            Images.AddRange(images);
        }
    }

    /// <summary>
    /// Добавить счетчик сообщений.
    /// </summary>
    public void AddViewsCount()
    {
        ViewsCount = new BulletinViewsCountCreateDtoWhileBulletinCreating { ViewsCount = 0 };
    }
}
