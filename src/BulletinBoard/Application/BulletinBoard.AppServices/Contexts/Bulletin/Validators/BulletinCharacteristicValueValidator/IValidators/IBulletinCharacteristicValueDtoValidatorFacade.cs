using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinCharacteristicValue
/// </summary>
public interface IBulletinCharacteristicValueDtoValidatorFacade : IValidatorFacade<BulletinCharacteristicValueCreateDto, BulletinCharacteristicValueUpdateDtoForValidating>
{
}
