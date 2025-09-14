namespace BulletinBoard.Contracts.Bulletin.BulletinCharacteristicName;

/// <summary>
/// Базовый формат характеристики
/// </summary>
public class BulletinsCharacteristicDto
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
