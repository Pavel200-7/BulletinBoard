using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;

/// <summary>
/// Формат данных создания данных сопоставления характеристики с объявлением.
/// Используется, при создании объявления, когда его id еще нет.
/// </summary>
public class BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating
{
    /// <summary>
    /// Id характеристики
    /// </summary>
    public Guid CharacteristicId { get; set; }

    /// <summary>
    /// Id одного из возможных значений характеристики
    /// </summary>
    public Guid CharacteristicValueId { get; set; }
}
