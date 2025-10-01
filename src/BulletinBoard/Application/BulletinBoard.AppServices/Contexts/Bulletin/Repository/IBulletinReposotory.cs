using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.ReadDto;
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
    /// Создать агрегат BulletinDto (объявление и все сопутствующие сущности).
    /// </summary>
    /// <param name="createDto">Дто создания объявления (агрегат).</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    public Task<Guid> CreateAsync(BelletinCreateDto createDto, CancellationToken cancellationToken);


}
