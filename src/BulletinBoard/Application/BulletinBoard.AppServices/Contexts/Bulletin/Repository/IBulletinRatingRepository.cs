using BulletinBoard.AppServices.Contexts.Bulletin.Repository.IBaseRepository;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinRating
/// </summary>
public interface IBulletinRatingRepository : IBaseRepository
    <
    BulletinRating,
    BulletinRatingDto,
    BulletinRatingCreateDto,
    BulletinRatingUpdateDto
    >
{
}
