namespace BulletinBoard.Contracts.Bulletin.BulletinImage.UpdateDto;

/// <summary>
/// Формат данных обновления изображения объявления
/// </summary>
public class BulletinImageUpdateDto
{
    /// <summary>
    /// Является ли изображение титульным 
    /// </summary>
    public bool IsMain { get; set; }
}
