using BulletinBoard.AppServices.Contexts.Bulletin.Repository.IBaseRepository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinsCharacteristic
/// </summary>
public interface IBulletinCharacteristicComparisonRepository : IBaseBulletinRepository
    <
    BulletinCharacteristicComparison,
    BulletinCharacteristicComparisonDto,
    BulletinCharacteristicComparisonCreateDto,
    BulletinCharacteristicComparisonUpdateDto
    >
{
}
