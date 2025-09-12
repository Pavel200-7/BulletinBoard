namespace BulletinBoard.Contracts.Bulletin.BulletinCategory;

/// <summary>
/// Формат данных для фильтрации категории по id родительской и (или) по названию
/// </summary>
public class BulletinCategoryFilterDto
{
    /// <summary>
    /// Используется ли id родительской категории для отбора
    /// </summary>
    public bool IsUsedParentCategoryId { get; set; }

    /// <summary>
    /// Id родительской категории
    /// </summary>
    public Guid? ParentCategoryId { get; set; }

    /// <summary>
    /// Используется ли название категории для отбора
    /// </summary>
    public bool IsUsedCategoryName { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public string CategoryName { get; set; }
}
