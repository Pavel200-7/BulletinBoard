namespace BulletinBoard.Contracts.Bulletin.BulletinImage;

/// <summary>
/// Базовый формат данных изображения объявления
/// </summary>
public class BulletinImageDto
{
    /// <summary>
    /// Id изображения объявления
    /// </summary>
    public Guid Id { get; set; }

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
