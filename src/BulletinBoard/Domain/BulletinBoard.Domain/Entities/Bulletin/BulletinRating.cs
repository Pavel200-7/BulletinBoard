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
    /// Рейтинг
    /// </summary>
    public decimal Rating { get; set; }

    /// <summary>
    /// Количество просмотров объявления
    /// </summary>
    public int ViewsCount { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к объявлению
    /// </summary>
    public BulletinMain Bulletin { get; set; }
}
