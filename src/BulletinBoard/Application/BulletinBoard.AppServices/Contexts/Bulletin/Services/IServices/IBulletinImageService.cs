using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.Contracts.Bulletin.BulletinImage;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис для работы с изображениями объявления
/// </summary>
public interface IBulletinImageService : IBaseCRUDService
    <
    BulletinImageDto,
    BulletinImageCreateDto,
    BulletinImageUpdateDto
    >
{
}
