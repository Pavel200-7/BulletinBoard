namespace BulletinBoard.Contracts.Bulletin.BelletinMain;

/// <summary>
/// Формат данных создания объявления
/// </summary>
public class BulletinMainCreateDto
{
    /// <summary>
    /// Id пользователя - создателя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Заголовок объявления
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Описание объявления
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Id категории, к которой относится объявление
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Цена товара или услуги, указанного в объявлении
    /// </summary>
    public decimal Price { get; set; }
}
