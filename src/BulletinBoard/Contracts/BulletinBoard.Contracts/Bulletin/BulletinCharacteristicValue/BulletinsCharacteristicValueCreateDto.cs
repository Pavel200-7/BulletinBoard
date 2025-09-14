namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;

/// <summary>
/// Формат данных создания данных возможного значения характеристики
/// </summary>
public class BulletinsCharacteristicValueCreateDto
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
