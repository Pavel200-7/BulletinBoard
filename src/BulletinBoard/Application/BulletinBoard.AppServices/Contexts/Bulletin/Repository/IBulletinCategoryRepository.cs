using BulletinBoard.AppServices.Contexts.Bulletin.Repository.IBaseRepository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinCategory
/// </summary>
public interface IBulletinCategoryRepository : IBaseBulletinRepository
    <
    BulletinCategory,
    BulletinCategoryDto,
    BulletinCategoryCreateDto,
    BulletinCategoryUpdateDto
    >
{
    
}
