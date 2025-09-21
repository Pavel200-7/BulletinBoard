using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО обновления сопоставления характеристики с объявлением по правилам:
/// CharacteristicValueId:
///     1. Значение характеристики существует.
///     2. Значение характеристики соответствует характеристике.
/// </summary>
public interface IBulletinCharacteristicComparisonUpdateDtoValidator : IDtoValidator<BulletinCharacteristicComparisonUpdateDto>
{
}
