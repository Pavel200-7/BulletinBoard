namespace BulletinBoard.Contracts.Bulletin.BulletinViewsCount.UpdateDto;

/// <summary>
/// Формат данных создания количества просмотров 
/// </summary>
public class BulletinViewsCountUpdateDto
{
    /// <summary>
    /// Количество просмотров объявления
    /// </summary>
    public int ViewsCount { get; set; }
}
