using BulletinBoard.Domain.Base;


namespace BulletinBoard.Domain.Entities.Bulletin;

/// <summary>
/// Сущность объявления
/// </summary>
public class BulletinMain : EntityBase 
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

    /// <summary>
    /// Навигационное свойство для доступа к пользователю - владельцу объявления
    /// </summary>
    public BulletinUser User { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к связанной категории
    /// </summary>
    public BulletinCategory Category { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к списку характеристик объявления
    /// </summary>
    public List<BulletinCharacteristicComparison> Characteristics { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к рейтингу
    /// </summary>
    public BulletinRating Rating { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к списку изображений 
    /// (вернее информации о них, сами изображения хранятся в другом домене)
    /// </summary>
    public List<BulletinImage> Images { get; set; }
}
