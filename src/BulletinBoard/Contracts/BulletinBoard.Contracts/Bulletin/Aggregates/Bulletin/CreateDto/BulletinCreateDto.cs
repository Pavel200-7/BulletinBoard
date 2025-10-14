using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.CreateDto;


namespace BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;

/// <summary>
/// Предназначен для операции создания объявления со всеми его сопутствующими связями (дополняющими сущностями).
/// </summary>
public class BulletinCreateDto
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
    public BulletinCreateDto(BulletinMainCreateDto main)
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
    public void AddImages(List<BulletinImageCreateDtoWhileBulletinCreating>?  images)
    {
        if (images is null || !images.Any()) {  return; }

        if (IsMainImagesMoreThanOne(images))
        {
            MakeMainOnlyLastMainImage(images);
        }

        Images.AddRange(images);
    }

    /// <summary>
    /// Больше ли одного главного (титульного) изображения.
    /// </summary>
    /// <param name="images">информация избражений.</param>
    /// <returns>Результат проверки.</returns>
    private bool IsMainImagesMoreThanOne(List<BulletinImageCreateDtoWhileBulletinCreating> images)
    {
        return images.Count(i => i.IsMain) > 1;
    }

    /// <summary>
    /// Оставить только 1 главное (титульное изображение) (последнее).
    /// </summary>
    /// <param name="images">информация избражений.</param>
    private void MakeMainOnlyLastMainImage(List<BulletinImageCreateDtoWhileBulletinCreating> images)
    {
        int lastMainIndex = -1;
        for (int i = images.Count - 1; i >= 0; i--)
        {
            if (images[i].IsMain)
            {
                lastMainIndex = i;
                break;
            }
        }

        if (lastMainIndex >= 0)
        {
            for (int i = 0; i < images.Count; i++)
            {
                if (i != lastMainIndex)
                {
                    images[i].IsMain = false;
                }
            }
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
