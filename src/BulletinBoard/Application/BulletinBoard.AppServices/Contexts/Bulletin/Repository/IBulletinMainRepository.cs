using BulletinBoard.AppServices.Contexts.Bulletin.Repository.IBaseRepository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinMain
/// </summary>
public interface IBulletinMainRepository : IBaseBulletinRepository
    <
    BulletinMain,
    BulletinMainDto,
    BulletinMainCreateDto,
    BulletinMainUpdateDto
    >
{
}
