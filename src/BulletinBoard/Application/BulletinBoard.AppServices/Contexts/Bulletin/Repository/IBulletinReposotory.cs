using BulletinBoard.Contracts.Bulletin.Agrigates.Belletin;
using BulletinBoard.Contracts.Bulletin.Agrigates.Bulletin.CreateDto;
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
    public Task<BelletinDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Создать агрегат BulletinDto (объявление и все сопутствующие сущности).
    /// </summary>
    /// <param name="createDto">Дто создания объявления (агрегат).</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    public Task<Guid> CreateAsync(BelletinCreateDto createDto, CancellationToken cancellationToken);


}
