using BulletinBoard.AppServices.Contexts.Bulletin.Repository.IBaseRepository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinRating
/// </summary>
public interface IBulletinRatingRepository : IBaseBulletinRepository
    <
    BulletinRating,
    BulletinRatingDto,
    BulletinRatingCreateDto,
    BulletinRatingUpdateDto
    >
{
}
