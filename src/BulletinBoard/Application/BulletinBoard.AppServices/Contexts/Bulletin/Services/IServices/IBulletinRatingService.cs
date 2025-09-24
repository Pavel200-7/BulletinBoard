using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.Contracts.Bulletin.BulletinRating;


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
