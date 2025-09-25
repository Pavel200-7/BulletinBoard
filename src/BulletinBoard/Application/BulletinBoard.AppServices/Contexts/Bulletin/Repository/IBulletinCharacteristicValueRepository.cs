using BulletinBoard.AppServices.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.UpdateDto;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinsCharacteristicValue
/// </summary>
public interface IBulletinCharacteristicValueRepository : IBaseRepository
    <
    BulletinCharacteristicValue,
    BulletinCharacteristicValueDto,
    BulletinCharacteristicValueCreateDto,
    BulletinCharacteristicValueUpdateDto
    >
{
}
