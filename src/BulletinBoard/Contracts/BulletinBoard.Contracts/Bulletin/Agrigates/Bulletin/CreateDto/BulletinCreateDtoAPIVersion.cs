using BulletinBoard.Contracts.Bulletin.BelletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.Agrigates.Bulletin.CreateDto;

/// <summary>
/// Предназначен для операции создания объявления со всеми его сопутствующими связями (дополняющими сущностями).
/// Используется на уровне контроллера.
/// Не включает в себя часть данных.
/// </summary>
public class BulletinCreateDtoAPIVersion
{
    /// <summary>
    /// Основная сущность объявления
    /// </summary>
    public BulletinMainCreateDto BulletinMain { get; set; }

    /// <summary>
    /// Характеристики объявления
    /// </summary>
    public List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating> CharacteristicComparisons { get; set; }
}
