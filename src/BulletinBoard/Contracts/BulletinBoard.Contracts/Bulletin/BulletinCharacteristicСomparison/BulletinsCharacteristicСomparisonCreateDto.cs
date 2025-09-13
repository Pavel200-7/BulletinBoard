namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;

/// <summary>
/// Формат данных создания данных сопоставления характеристики с объявлением
/// </summary>
public class BulletinsCharacteristicСomparisonCreateDto
{
    /// <summary>
    /// Id объявления
    /// </summary>
    public Guid BelletinId { get; set; }

    /// <summary>
    /// Id характеристики
    /// </summary>
    public Guid CharacteristicNameId { get; set; }

    /// <summary>
    /// Id одного из возможных значений характеристики
    /// </summary>
    public Guid CharacteristicValueId { get; set; }
}
