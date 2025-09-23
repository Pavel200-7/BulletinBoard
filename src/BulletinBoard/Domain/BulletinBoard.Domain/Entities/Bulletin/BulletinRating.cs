using BulletinBoard.Domain.Base;


namespace BulletinBoard.Domain.Entities.Bulletin;

/// <summary>
/// Сущность рейтинга объявления
/// </summary>
public class BulletinRating : EntityBase
{
    /// <summary>
    /// Id объявления
    /// </summary>
    public Guid BulletinId { get; set; }

    /// <summary>
    /// Id пользователя, оставившего отзыв.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Рейтинг
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Время создания.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к объявлению
    /// </summary>
    public BulletinMain Bulletin { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к пользовалелю.
    /// </summary>
    public BulletinUser BulletinUser { get; set; }
}
