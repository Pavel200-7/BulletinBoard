using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.CreateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;

/// <summary>
/// Предназначен для операции создания объявления со всеми его сопутствующими связями (дополняющими сущностями).
/// Передается из контроллера.
/// Тут из сессии нужно передать данные изображенияи все то, что есть в BulletinCreateDtoRequest.
/// </summary>
public class BulletinCreateDtoController
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
    public List<BulletinImageCreateDtoWhileBulletinCreating> Images { get; set; }

    /// <summary>
    /// Конструктор для контроллера.
    /// </summary>
    public BulletinCreateDtoController()
    {
        CharacteristicComparisons = new List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating>();
        Images = new List<BulletinImageCreateDtoWhileBulletinCreating>();
    }

    /// <summary>
    /// Создать объект объявления с главной частью и характеристиками.
    /// </summary>
    /// <param name="bulletinCreateDtoRequest">Данные объявления</param>
    public BulletinCreateDtoController(BulletinCreateDtoRequest bulletinCreateDtoRequest)
    {
        BulletinMain = bulletinCreateDtoRequest.BulletinMain;
        CharacteristicComparisons = bulletinCreateDtoRequest.CharacteristicComparisons;
        Images = new();
    }

    /// <summary>
    /// Добавить id изображений.
    /// </summary>
    /// <param name="imagesIslist">Данные изображений.</param>
    public void AddImages(List<BulletinImageCreateDtoWhileBulletinCreating>? imagesIslist)
    {
        if (imagesIslist is null || !imagesIslist.Any()) { return; }
        Images.AddRange(imagesIslist);
    }
}
