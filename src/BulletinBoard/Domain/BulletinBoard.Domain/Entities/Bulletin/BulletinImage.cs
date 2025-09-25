using BulletinBoard.Domain.Base;


namespace BulletinBoard.Domain.Entities.Bulletin;

/// <summary>
/// Сущность для доступа к сущности домена, пердназначенного для 
/// хранения и чнетия изображений.
/// Хранит id этой сущности.
/// </summary>
public class BulletinImage : EntityBase
{
    /// <summary>
    /// Id объявления
    /// </summary>
    public Guid BulletinId { get; set; }

    /// <summary>
    /// Является ли изображение титульным 
    /// </summary>
    public bool IsMain { get; set; }

    /// <summary>
    /// Время создания
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к объявлению
    /// </summary>
    public BulletinMain Bulletin { get; set; }
}
