using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinCharacteristicComparison
/// </summary>
public interface IBulletinCharacteristicComparisonDtoValidatorFacade : IValidatorFacade<BulletinCharacteristicComparisonCreateDto, BulletinCharacteristicComparisonUpdateDto>
{
}
