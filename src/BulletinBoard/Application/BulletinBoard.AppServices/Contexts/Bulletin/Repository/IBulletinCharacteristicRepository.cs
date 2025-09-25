using BulletinBoard.AppServices.Repository;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.UpdateDto;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Repository;

/// <summary>
/// Репозиторий для доступа к сущности BulletinsCharacteristicName
/// </summary>
public interface IBulletinCharacteristicRepository : IBaseRepository
    <
    BulletinCharacteristic,
    BulletinCharacteristicDto,
    BulletinCharacteristicCreateDto,
    BulletinCharacteristicUpdateDto
    >
{ 
}
