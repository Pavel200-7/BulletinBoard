namespace BulletinBoard.Contracts.Bulletin.BulletinImage;

/// <summary>
/// Формат данных добавления изображения объявления
/// </summary>
public class BulletinImageCreateDto
{
    /// <summary>
    /// Id изображения.
    /// Является копией id изображения из другого домена,
    /// который предназначен для храния изображений в BLOB полях БД.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Id объявления
    /// </summary>
    public Guid BulletinId { get; set; }

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
}
