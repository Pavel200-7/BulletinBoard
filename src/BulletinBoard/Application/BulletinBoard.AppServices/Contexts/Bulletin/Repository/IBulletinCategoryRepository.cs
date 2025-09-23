using BulletinBoard.AppServices.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinCategory
/// </summary>
public interface IBulletinCategoryRepository : IBaseRepository
    <
    BulletinCategory,
    BulletinCategoryDto,
    BulletinCategoryCreateDto,
    BulletinCategoryUpdateDto
    >
{
}
