namespace BulletinBoard.Contracts.Bulletin.BulletinRating;

/// <summary>
/// Базовый формат данных рейтинга объявления
/// </summary>
public class BulletinRatingDto
{
    /// <summary>
    /// Id объявления
    /// </summary>
    public Guid BulletinId { get; set; }

    /// <summary>
    /// Рейтинг
    /// </summary>
    public decimal Rating { get; set; }

    /// <summary>
    /// Количество просмотров объявления
    /// </summary>
    public int VievsCount { get; set; }
}
