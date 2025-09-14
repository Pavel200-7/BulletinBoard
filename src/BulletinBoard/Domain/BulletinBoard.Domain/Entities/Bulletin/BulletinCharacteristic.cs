using BulletinBoard.Domain.Base;


namespace BulletinBoard.Domain.Entities.Bulletin;

/// <summary>
/// Сущность характеристики объявления
/// </summary>
public class BulletinCharacteristic : EntityBase
{
    /// <summary>
    /// Наименование характеристики
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Id Категории объявления
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к связанной категории
    /// </summary>
    public BulletinCategory Category { get; set; }

    /// <summary>
    /// Навигационное свойство для доспупа к списку возможных значений характеристики
    /// </summary>
    public List<BulletinCharacteristicValue> CharacteristicValues { get; set; }

    /// <summary>
    /// Навигационное свойства для доступа ко всем записям сопоставления характеристики и объявления,
    /// где была использована данная сущность.
    /// Использоваться оно не будет, но лучше указать его наличие явно.
    /// </summary>
    public List<BulletinCharacteristicСomparison> CharacteristicСomparisons { get; set; }
}
