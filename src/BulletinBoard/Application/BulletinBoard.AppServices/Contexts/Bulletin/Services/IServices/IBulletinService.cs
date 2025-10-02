using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.FilterDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.ReadDto;


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
    public Task<BulletinDto> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить объявление (как совокупности связанных сущностей) по id.
    /// Из ДТО, получаемого здесь исключена ненужная (для операций только для чтения) информация.
    /// </summary>
    /// <param name="id">Id объявления.</param>
    /// <returns>Данные объявления (без избыточных).</returns>
    public Task<BulletinReadSingleDto> GetByIdReadSingleAsync(Guid id);

    /// <summary>
    /// Получить отсортированную и отфильтрованную выборку объявлений (страницу).
    /// </summary>
    /// <param name="requestDto">Запрос на получение выборки.</param>
    /// <returns>Коллекция данных объявления.</returns>
    public Task<BulletinReadPagenatedDto> GetBulletinsAsync(BulletinPaginationRequestDto requestDto);

    /// <summary>
    /// Создать объявление.
    /// </summary>
    /// <param name="createDto">Данные создания объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Id объявления.</returns>
    public Task<Guid> CreateAsync(BulletinCreateDtoController createDto, CancellationToken cancellationToken);
}
