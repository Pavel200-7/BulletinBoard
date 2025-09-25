namespace BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;

/// <summary>
/// Формат данных добавления изображения объявления.
/// Используется, при создании объявления, когда его id еще нет.
/// </summary>
public class BulletinImageCreateDtoWhileBulletinCreating
{
    /// <summary>
    /// Id изображения.
    /// Является копией id изображения из другого домена,
    /// который предназначен для храния изображений в BLOB полях БД.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Является ли изображение титульным
    /// </summary>
    public bool IsMain { get; set; }
}
