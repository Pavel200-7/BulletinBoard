using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.Contracts.Bulletin.Agrigates.Belletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinAgrigateService : IBulletinAgrigateService
{
    /// <inheritdoc/>
    public Task<BelletinDto> CreateAsync(BelletinCreateDto createDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
