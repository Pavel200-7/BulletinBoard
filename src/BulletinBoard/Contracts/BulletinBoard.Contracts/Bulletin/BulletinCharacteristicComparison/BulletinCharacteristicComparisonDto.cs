namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;

/// <summary>
/// Базовый формат данных сопоставления характеристики с объявлением
/// </summary>
public class BulletinCharacteristicComparisonDto
{
    /// <summary>
    /// Id сопоставления объявления и характеристики
    /// </summary>
    public Guid Id { get; set; }

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
}
