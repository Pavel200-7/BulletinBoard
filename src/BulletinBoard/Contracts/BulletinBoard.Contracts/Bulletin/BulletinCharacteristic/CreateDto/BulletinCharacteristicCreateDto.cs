namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.CreateDto;

/// <summary>
/// Формат данных создания характеристики
/// </summary>
public class BulletinCharacteristicCreateDto
{
    /// <summary>
    /// Наименование характеристики
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Id Категории объявления
    /// </summary>
    public Guid CategoryId { get; set; }

}
