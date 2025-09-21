using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.ForValidating;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinCharacteristic.
/// </summary>
public interface IBulletinCharacteristicDtoValidatorFacade : IValidatorFacade<BulletinCharacteristicCreateDto, BulletinCharacteristicUpdateDtoForValidating>
{
}
