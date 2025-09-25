using BulletinBoard.AppServices.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinImages
/// </summary>
public interface IBulletinImageRepository : IBaseRepository
    <
    BulletinImage,
    BulletinImageDto,
    BulletinImageCreateDto,
    BulletinImageUpdateDto
    >
{



}
