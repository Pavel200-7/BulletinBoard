namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;

/// <summary>
/// Формат данных для фильтрации характеристик по
///     1. Требует доработки
/// </summary>
public class BulletinCharacteristicFilterDto
{
    /// <summary>
    /// Используется ли имя для отбора.
    /// </summary>
    public bool IsUsedName { get; set; }

    /// <summary>
    /// Используется ли имя для отбора по частичному совпадению.
    /// </summary>
    public bool IsUsedNameContains { get; set; }

    /// <summary>
    /// Наименование характеристики
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Используется ли CategoryId для отбора по частичному совпадению.
    /// </summary>
    public bool IsUsedCategory { get; set; }

    /// <summary>
    /// Id Категории объявления
    /// </summary>
    public Guid CategoryId { get; set; }
}
