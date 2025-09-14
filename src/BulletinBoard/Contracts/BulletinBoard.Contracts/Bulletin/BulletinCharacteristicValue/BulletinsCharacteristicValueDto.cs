namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;

/// <summary>
/// Базовый формат данных возможного значений характеристики
/// </summary>
public class BulletinsCharacteristicValueDto
{
    /// <summary>
    /// Id возможного значения характеристики
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Id характеристики
    /// </summary>
    public Guid CharacteristicId { get; set; }

    /// <summary>
    /// Возможное значение характеристики
    /// </summary>
    public string Value { get; set; }
}
