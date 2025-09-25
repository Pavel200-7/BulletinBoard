using BulletinBoard.Contracts.Bulletin.Agrigates.Belletin;
using BulletinBoard.Contracts.Bulletin.Agrigates.Bulletin.CreateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис создания объявления (как совокупности связанных сущностей).
/// </summary>
public interface IBulletinService
{
    /// <summary>
    /// Создать объявление.
    /// </summary>
    /// <param name="createDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns> Id объявления.</returns>
    public Task<Guid> CreateAsync(BelletinCreateDto createDto, CancellationToken cancellationToken);
}
