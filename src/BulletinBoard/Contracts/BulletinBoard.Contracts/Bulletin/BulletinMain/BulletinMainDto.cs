namespace BulletinBoard.Contracts.Bulletin.BulletinMain;

/// <summary>
/// Базовый формат данных объявления
/// </summary>
public class BulletinMainDto
{
    /// <summary>
    /// Id объявления
    /// </summary>
    public Guid Id { get; set; }

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
    /// </summary>}
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Цена товара или услуги, указанного в объявлении
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Время создания
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Скрато ли от просмотра
    /// </summary>
    public bool Hidden { get; set; }

    /// <summary>
    /// Скрато ли от просмотра по причине завершения действия
    /// </summary>
    public bool Closed { get; set; }

    /// <summary>
    /// Скрато ли от просмотра по причине блокировки со стороны администрации
    /// </summary>
    public bool Blocked { get; set; }
}
