using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.ReadDto;


/// <summary>
/// Характеристика объявления с ее значением. 
/// Используется при чтении информации объявления.
/// </summary>
public class BulletinCharacteristicComparisonBulletinReadDto
{
    /// <summary>
    /// Id сопоставления объявления и характеристики
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Характеристика
    /// </summary>
    public String Characteristic { get; set; }


    /// <summary>
    /// Значение характеристики.
    /// </summary>
    public String CharacteristicValue { get; set; }
}
