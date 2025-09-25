using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BelletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BelletinMain.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис для работы с объявлениями
/// </summary>
public interface IBulletinMainService : IBaseCRUDService
    <
    BulletinMainDto,
    BulletinMainCreateDto,
    BulletinMainUpdateDto
    >
{
}
