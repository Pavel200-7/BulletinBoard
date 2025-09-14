namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristicName;

/// <summary>
/// Формат данных создания характеристики
/// </summary>
public class BulletinsCharacteristicCreateDto
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
