using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Contracts.Bulletin.BulletinRating.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис для работы с рейтингами
/// </summary>
public interface IBulletinRatingService : IBaseCRUDService
    <
    BulletinRatingDto,
    BulletinRatingCreateDto,
    BulletinRatingUpdateDto
    >
{
}
