using BulletinBoard.Domain.Base;


namespace BulletinBoard.Domain.Entities.Bulletin;


/// <summary>
/// Сущность для сопоставления объявления и всех его характеристик
/// </summary>
public class BulletinCharacteristicComparison : EntityBase
{
    /// <summary>
    /// Id объявления
    /// </summary>
    public Guid BulletinId { get; set; }

    /// <summary>
    /// Id характеристики
    /// </summary>
    public Guid CharacteristicId { get; set; }

    /// <summary>
    /// Id одного из возможных значений характеристики
    /// </summary>
    public Guid CharacteristicValueId { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к объявлению
    /// </summary>
    public BulletinMain Bulletin { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к характеристике
    /// </summary>
    public BulletinCharacteristic Characteristic { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к значению характеристики
    /// </summary>
    public BulletinCharacteristicValue CharacteristicValue {  set; get; }
}
