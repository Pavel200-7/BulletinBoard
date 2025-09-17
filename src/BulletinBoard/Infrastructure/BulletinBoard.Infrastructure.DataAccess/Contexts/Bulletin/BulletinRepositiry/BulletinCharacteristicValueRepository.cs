using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry.Base;
using BulletinBoard.Infrastructure.DataAccess.Repositories;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

/// <inheritdoc/>
public class BulletinCharacteristicValueRepository :
    BaseRepository
    <
    BulletinCharacteristicValue,
    BulletinCharacteristicValueDto,
    BulletinCharacteristicValueCreateDto,
    BulletinCharacteristicValueUpdateDto,
    BulletinContext
    >,
    IBulletinCharacteristicValueRepository
{
    public BulletinCharacteristicValueRepository(IRepository<BulletinCharacteristicValue, BulletinContext> repository, IMapper mapper)
        : base(repository, mapper)
    {
    }
}