using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис для работы с счетчиком просмотров.
/// </summary>
public interface IBulletinViewsCountService : IBaseCRUDService
    <
    BulletinViewsCountDto,
    BulletinViewsCountCreateDto,
    BulletinViewsCountUpdateDto
    >
{
    /// <summary>
    /// Увеличить количество просмотров объявления на 1.
    /// </summary>
    /// <param name="bulletinId">id объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Базовый формат данных счетчика просмотров.</returns>
    public Task<BulletinViewsCountDto> IncreaseViewsCountAsync(Guid bulletinId, CancellationToken cancellationToken);
}
