namespace BulletinBoard.Contracts.Bulletin.BulletinImage;

/// <summary>
/// Формат данных добавления изображения объявления
/// </summary>
public class BulletinImageCreateDto
{
    /// <summary>
    /// Id объявления
    /// </summary>
    public Guid BelletinId { get; set; }

    /// <summary>
    /// Является ли изображение титульным
    /// </summary>
    public bool IsMain { get; set; }

    /// <summary>
    /// Название изображения
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Время создания
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Некоторый путь к изображению
    /// </summary>
    public string Path { get; set; }
}
