using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.UpdateDto;
using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Repositories;


namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin.BulletinRepositiry;

/// <inheritdoc/>
public class BulletinCharacteristicComparisonRepository :
    BaseRepository
    <
    BulletinCharacteristicComparison,
    BulletinCharacteristicComparisonDto,
    BulletinCharacteristicComparisonCreateDto,
    BulletinCharacteristicComparisonUpdateDto,
    BulletinContext
    >,
    IBulletinCharacteristicComparisonRepository
{
    public BulletinCharacteristicComparisonRepository(IRepository<BulletinCharacteristicComparison, BulletinContext> repository, IMapper mapper)
        : base(repository, mapper)
    {
    }
}
