using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.FilterDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.ReadDto;
using BulletinBoard.Domain.Entities.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для работы с агрегатом BulletinDto.
/// </summary>
public interface IBulletinReposotory
{
    /// <summary>
    /// Получить объявление (как совокупности связанных сущностей) по id.
    /// </summary>
    /// <param name="id">Id объявления.</param>
    /// <returns>Данные объявления.</returns>
    public Task<BulletinDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить объявление (как совокупности связанных сущностей) по id.
    /// Из ДТО, получаемого здесь исключена ненужная (для операций только для чтения) информация.
    /// </summary>
    /// <param name="id">Id объявления.</param>
    /// <returns>Данные объявления (без избыточных).</returns>
    public Task<BulletinReadSingleDto?> GetByIdReadSingleAsync(Guid id);

    /// <summary>
    /// Получить отсортированную и отфильтрованную выборку объявлений (страницу).
    /// </summary>
    /// <param name="requestDto">Запрос на получение выборки.</param>
    /// <returns>Коллекция данных объявления.</returns>
    public Task<IReadOnlyCollection<BulletinReadPagenatedItemDto>> GetBulletinsAsync(ExtendedSpecification<BulletinMain> specification);

    /// <summary>
    /// Создать агрегат BulletinDto (объявление и все сопутствующие сущности).
    /// </summary>
    /// <param name="createDto">Дто создания объявления (агрегат).</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    public Task<Guid> CreateAsync(BulletinCreateDto createDto, CancellationToken cancellationToken);


}
