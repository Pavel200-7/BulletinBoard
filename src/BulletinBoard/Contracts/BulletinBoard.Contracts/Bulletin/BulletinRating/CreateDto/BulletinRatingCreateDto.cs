namespace BulletinBoard.Contracts.Bulletin.BulletinRating.CreateDto;

/// <summary>
/// Формат данных создания рейтинга объявления
/// </summary>
public class BulletinRatingCreateDto
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
}
