using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.Contracts.Bulletin.BulletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.UpdateDto;


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
    /// <summary>
    /// Заблокировать объявление
    /// </summary>
    /// <param name="id">id объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    public Task<BulletinMainDto> BlockBulletin(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Разблокировать объявление.
    /// </summary>
    /// <param name="id">id объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    public Task<BulletinMainDto> UnBlockBulletin(Guid id, CancellationToken cancellationToken);
}
