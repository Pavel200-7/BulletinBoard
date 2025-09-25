namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;

/// <summary>
/// Формат данных создания данных сопоставления характеристики с объявлением
/// </summary>
public class BulletinCharacteristicComparisonCreateDto
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
}
