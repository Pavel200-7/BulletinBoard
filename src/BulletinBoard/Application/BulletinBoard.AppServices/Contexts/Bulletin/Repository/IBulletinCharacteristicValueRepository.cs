using BulletinBoard.AppServices.Contexts.Bulletin.Repository.IBaseRepository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicName;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinsCharacteristicValue
/// </summary>
public interface IBulletinCharacteristicValueRepository : IBaseBulletinRepository
    <
    BulletinCharacteristicValue,
    BulletinCharacteristicValueDto,
    BulletinCharacteristicValueCreateDto,
    BulletinCharacteristicValueUpdateDto
    >
{
}
