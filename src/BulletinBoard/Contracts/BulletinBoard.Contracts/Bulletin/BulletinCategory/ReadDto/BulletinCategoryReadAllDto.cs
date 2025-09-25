namespace BulletinBoard.Contracts.Bulletin.BulletinCategory.ReadDto;

/// <summary>
/// Формат данных для вывода всех категорий в их правильном иерархическом виде
/// </summary>
public class BulletinCategoryReadAllDto
{
    /// <summary>
    /// Id категории
    /// </summary>
    public Guid? Id { get; set; }

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
    /// Множесво дочерних категорий
    /// </summary>
    public List<BulletinCategoryReadAllDto> ChildrenCategories { get; set; }
}
