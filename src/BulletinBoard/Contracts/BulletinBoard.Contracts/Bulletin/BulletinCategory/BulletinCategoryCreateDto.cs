namespace BulletinBoard.Contracts.Bulletin.BulletinCategory;

/// <summary>
/// Формат данных создания категории
/// </summary>
public class BulletinCategoryCreateDto
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
}
