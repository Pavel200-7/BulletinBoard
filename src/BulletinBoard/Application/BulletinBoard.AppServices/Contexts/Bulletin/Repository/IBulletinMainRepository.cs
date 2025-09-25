using BulletinBoard.AppServices.Repository;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BelletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BelletinMain.UpdateDto;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinMain
/// </summary>
public interface IBulletinMainRepository : IBaseRepository
    <
    BulletinMain,
    BulletinMainDto,
    BulletinMainCreateDto,
    BulletinMainUpdateDto
    >
{
}
