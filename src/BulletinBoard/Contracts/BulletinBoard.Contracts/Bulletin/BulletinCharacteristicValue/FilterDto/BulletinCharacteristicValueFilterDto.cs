namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.FilterDto;

/// <summary>
/// Формат данных для фильтрации данных возможного значения характеристики по
///     1. Требует доработки
/// </summary>
public class BulletinCharacteristicValueFilterDto
{
    /// <summary>
    /// Используется ли CharacteristicId.
    /// </summary>
    public bool IsUsedCharacteristicId { get; set; }

    /// <summary>
    /// Id характеристики
    /// </summary>
    public Guid CharacteristicId { get; set; }

    /// <summary>
    /// Используется ли Value.
    /// </summary>
    public bool IsUsedValue { get; set; }

    /// <summary>
    /// Используется ли отбор по частичному совпадению Value.
    /// </summary>
    public bool IsUsedValueContains { get; set; }

    /// <summary>
    /// Возможное значение характеристики
    /// </summary>
    public string Value { get; set; }
}
