using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.UpdateDto;

/// <summary>
/// ДТО для валидации изменения.
/// </summary>
public class BulletinCharacteristicValueUpdateDtoForValidating
{
    /// <summary>
    /// Id характеристики
    /// </summary>
    public Guid CharacteristicId { get; set; }

    /// <summary>
    /// Возможное значение характеристики
    /// </summary>
    public string Value { get; set; }
}
