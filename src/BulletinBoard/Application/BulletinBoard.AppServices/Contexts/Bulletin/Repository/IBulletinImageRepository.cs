using BulletinBoard.AppServices.Contexts.Bulletin.Repository.IBaseRepository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinImages
/// </summary>
public interface IBulletinImageRepository : IBaseBulletinRepository
    <
    BulletinImage,
    BulletinImageDto,
    BulletinImageCreateDto,
    BulletinImageUpdateDto
    >
{
}
