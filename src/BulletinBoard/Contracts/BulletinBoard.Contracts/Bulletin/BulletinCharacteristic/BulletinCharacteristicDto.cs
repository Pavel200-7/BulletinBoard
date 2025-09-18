namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;

/// <summary>
/// Базовый формат данных характеристики
/// </summary>
public class BulletinCharacteristicDto
{
    /// <summary>
    /// Id характеристики
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование характеристики
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Id Категории объявления
    /// </summary>
    public Guid CategoryId { get; set; }
}
