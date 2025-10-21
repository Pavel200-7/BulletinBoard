using BulletinBoard.AppServices.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.UpdateDto;
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
    /// <summary>
    /// Выставить статус блокировки объявления.
    /// </summary>
    /// <param name="id">id объявления.</param>
    /// <param name="blockStatus">статус (заблокировано)</param>
    /// <param name="cancellation">Токен отмены</param>
    /// <returns></returns>
    public Task<BulletinMainDto?> SetBulletinBlockStatusAsync(Guid id, bool blockStatus, CancellationToken cancellation);
}
