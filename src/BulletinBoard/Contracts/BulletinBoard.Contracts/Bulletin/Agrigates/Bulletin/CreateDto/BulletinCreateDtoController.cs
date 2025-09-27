using BulletinBoard.Contracts.Bulletin.BelletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.CreateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.Agrigates.Bulletin.CreateDto;

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
}
