namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.UpdateDto;

/// <summary>
/// Формат данных обновления данных сопоставления характеристики с объявлением
/// </summary>
public class BulletinCharacteristicComparisonUpdateDto
{
    /// <summary>
    /// Id одного из возможных значений характеристики
    /// </summary>
    public Guid CharacteristicValueId { get; set; }
}
