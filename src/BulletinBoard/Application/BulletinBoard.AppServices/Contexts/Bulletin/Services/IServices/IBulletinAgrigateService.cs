using BulletinBoard.Contracts.Bulletin.Agrigates.Belletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис создания объявления (как совокупности связанных сущностей).
/// </summary>
public interface IBulletinAgrigateService
{
    /// <summary>
    /// Создать объявление.
    /// </summary>
    /// <param name="createDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<BelletinDto> CreateAsync(BelletinCreateDto createDto, CancellationToken cancellationToken);
}
