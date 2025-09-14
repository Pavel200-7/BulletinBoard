using BulletinBoard.Domain.Base;


namespace BulletinBoard.Domain.Entities.Bulletin;

/// <summary>
/// Сущность категории объявления
/// </summary>
public class BulletinCategory : EntityBase
{
    /// <summary>
    /// Id родительской категории
    /// </summary>
    public Guid? ParentCategoryId { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public string CategoryName { get; set; }

    /// <summary>
    /// Является ли листовой (типиковой) или может быть родительской
    /// </summary>
    public bool IsLeafy { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к связанной родительской категории
    /// </summary>
    public BulletinCategory ParentCategory { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к списку дочерних элементов
    /// </summary>
    public List<BulletinCategory> ChildrenCategories { get; set; }

    /// <summary>
    /// Навигационное свойство для доступа к объявлениям,
    /// относящимся к этой категории
    /// </summary>
    public List<BulletinMain> Bulletins { get; set; }    

    /// <summary>
    /// Навигационное свойство для доступа к списку характеристик, пригодных для данной категории.
    /// </summary>
    public List<BulletinCharacteristic> Characteristics { get; set; }
}
