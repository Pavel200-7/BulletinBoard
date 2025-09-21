using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО создания сопоставления характеристики с объявлением по правилам:
/// BulletinId:
///     1. Объявление существует.
///     2. Объявление не заблокировано.
/// CharacteristicId:
///     1. Характеристика существует.
///     2. Характеристика относится к той же категории, что и объявление.
///     3. У характеристику больше 0 возможных значений.
/// CharacteristicValueId:
///     1. Значение характеристики существует.
///     2. Значение характеристики соответствует характеристике. 
/// </summary>
public interface IBulletinCharacteristicComparisonCreateDtoValidator : IDtoValidator<BulletinCharacteristicComparisonCreateDto>
{
}
