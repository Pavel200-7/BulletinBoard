using BulletinBoard.AppServices.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.UpdateDto;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinsViewsCount
/// </summary>
public interface IBulletinViewsCountRepository : IBaseRepository
    <
    BulletinViewsCount,
    BulletinViewsCountDto,
    BulletinViewsCountCreateDto,
    BulletinViewsCountUpdateDto
    >
{
    /// <summary>
    /// Увеличить счетчик просмотров объявления на 1.
    /// </summary>
    /// <param name="id">id объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    public Task<BulletinViewsCountDto?> IncreaseViewsCountAsync(Guid id, CancellationToken cancellationToken);
}
