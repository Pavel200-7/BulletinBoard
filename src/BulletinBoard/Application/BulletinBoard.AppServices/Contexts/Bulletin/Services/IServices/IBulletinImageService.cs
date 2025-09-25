using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using System.Threading;


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
    /// <summary>
    /// Получить данные титульного изображения.
    /// </summary>
    /// <param name="bulletinId">Id объявления.</param>
    /// <returns>Титульное изображение или null, если его нет.</returns>
    public Task<BulletinImageDto?> GetMainAsync(Guid bulletinId);

    /// <summary>
    /// Установить статус "Титульный" изображения.
    /// </summary>
    /// <param name="id">id изображения.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Титульное изображение или null, если его нет.</returns>
    public Task<BulletinImageDto?> SetMain(Guid id, CancellationToken cancellationToken);
}
