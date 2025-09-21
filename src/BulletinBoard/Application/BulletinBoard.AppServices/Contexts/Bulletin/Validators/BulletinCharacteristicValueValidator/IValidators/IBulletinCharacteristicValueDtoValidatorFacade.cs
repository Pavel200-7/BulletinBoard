using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.ForValidating;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinCharacteristicValue
/// </summary>
public interface IBulletinCharacteristicValueDtoValidatorFacade : IValidatorFacade<BulletinCharacteristicValueCreateDto, BulletinCharacteristicValueUpdateDtoForValidating>
{
}
