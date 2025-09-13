namespace BulletinBoard.Contracts.Bulletin.BulletinRating;

/// <summary>
/// Формат данных создания рейтинга объявления
/// </summary>
public class BulletinRatingCreateDto
{
    /// <summary>
    /// Id объявления
    /// </summary>
    public Guid BulletinId { get; set; }
}
