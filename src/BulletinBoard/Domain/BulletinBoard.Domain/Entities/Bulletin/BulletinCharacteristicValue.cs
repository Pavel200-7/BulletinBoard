using BulletinBoard.Domain.Base;


namespace BulletinBoard.Domain.Entities.Bulletin;

/// <summary>
/// Сущность, содержащая одно из возможных значений характеристики объявления
/// </summary>
public class BulletinCharacteristicValue : EntityBase
{
    /// <summary>
    /// Id характеристики
    /// </summary>
    public Guid CharacteristicId { get; set; }

    /// <summary>
    /// Возможное значение характеристики
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к связанной характеристике
    /// </summary>
    public BulletinCharacteristic Characteristic { get; set; }

    /// <summary>
    /// Навигационное свойства для доступа ко всем записям сопоставления характеристики и объявления,
    /// где была использована данная сущность.
    /// Использоваться оно не будет, но лучше указать его наличие явно.
    /// </summary>
    public List<BulletinCharacteristicComparison> CharacteristicСomparisons { get; set; }
}
