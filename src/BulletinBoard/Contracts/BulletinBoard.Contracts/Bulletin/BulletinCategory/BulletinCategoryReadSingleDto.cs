namespace BulletinBoard.Contracts.Bulletin.BulletinCategory;

/// <summary>
/// Формат данных для вывода одной карегории в виде древовидной струкруры от самого корня
/// </summary>
public class BulletinCategoryReadSingleDto
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
    /// Дочерняя категория
    /// </summary>
    public BulletinCategoryReadSingleDto? ChildrenCategory { get; set; }
}
