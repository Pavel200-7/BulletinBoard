using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicName;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Repositories;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

/// <inheritdoc/>
public class BulletinCharacteristicRepository :
    BaseRepository
    <
    BulletinCharacteristic,
    BulletinCharacteristicDto,
    BulletinCharacteristicCreateDto,
    BulletinCharacteristicUpdateDto,
    BulletinContext
    >,
    IBulletinCharacteristicRepository
{
    public BulletinCharacteristicRepository(IRepository<BulletinCharacteristic, BulletinContext> repository, IMapper mapper)
        : base(repository, mapper)
    {
    }
}