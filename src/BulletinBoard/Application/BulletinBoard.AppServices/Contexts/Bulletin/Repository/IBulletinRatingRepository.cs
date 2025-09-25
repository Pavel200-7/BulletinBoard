using BulletinBoard.AppServices.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Contracts.Bulletin.BulletinRating.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.UpdateDto;
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
