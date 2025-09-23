namespace BulletinBoard.Contracts.Bulletin.BulletinRating;

/// <summary>
/// Базовый формат данных рейтинга объявления
/// </summary>
public class BulletinRatingDto
{
    /// <summary>
    /// Id рейтинга
    /// </summary>
    public Guid Id { get; set; }

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
}
