namespace BulletinBoard.Contracts.Bulletin.BulletinCategory;

/// <summary>
/// Формат данных изменения категории
/// </summary>
public class BulletinCategoryUpdateDto
{
    /// <summary>
    /// Id родительской категории
    /// </summary>
    public Guid? ParentCategoryId { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public string CategoryName { get; set; }
}
