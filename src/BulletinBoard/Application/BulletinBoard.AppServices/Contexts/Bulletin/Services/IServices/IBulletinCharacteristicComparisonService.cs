using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;

/// <summary>
/// Сервис для работы со связями характик с объявлеями.
/// </summary>
public interface IBulletinCharacteristicComparisonService : IBaseCRUDService
    <
    BulletinCharacteristicComparisonDto,
    BulletinCharacteristicComparisonCreateDto,
    BulletinCharacteristicComparisonUpdateDto
    >
{
}
