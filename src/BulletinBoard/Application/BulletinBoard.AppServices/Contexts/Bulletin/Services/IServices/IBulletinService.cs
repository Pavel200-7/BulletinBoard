using BulletinBoard.Contracts.Bulletin.Agrigates.Belletin;
using BulletinBoard.Contracts.Bulletin.Agrigates.Bulletin.CreateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис создания объявления (как совокупности связанных сущностей).
/// </summary>
public interface IBulletinService
{
    /// <summary>
    /// Получить объявление (как совокупности связанных сущностей) по id.
    /// </summary>
    /// <param name="id">Id объявления.</param>
    /// <returns>Данные объявления.</returns>
    public Task<BelletinDto> GetByIdAsync(Guid id);

    /// <summary>
    /// Создать объявление.
    /// </summary>
    /// <param name="createDto">Данные создания объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Id объявления.</returns>
    public Task<Guid> CreateAsync(BulletinCreateDtoController createDto, CancellationToken cancellationToken);
}
