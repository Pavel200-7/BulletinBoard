namespace BulletinBoard.Contracts.Bulletin.BulletinImage;

/// <summary>
/// Формат данных обновления изображения объявления
/// </summary>
public class BulletinImageUpdateDto
{
    /// <summary>
    /// Название изображения
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Является ли изображение титульным
    /// </summary>
    public bool IsMain { get; set; }
}
