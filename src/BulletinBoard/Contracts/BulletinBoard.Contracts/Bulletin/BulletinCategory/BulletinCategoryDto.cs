namespace BulletinBoard.Contracts.Bulletin.BulletinCategory;

/// <summary>
/// Базовый формат данных категории
/// </summary>
public class BulletinCategoryDto
{
    /// <summary>
    /// Id категории
    /// </summary>
    public Guid Id { get; set; }

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
