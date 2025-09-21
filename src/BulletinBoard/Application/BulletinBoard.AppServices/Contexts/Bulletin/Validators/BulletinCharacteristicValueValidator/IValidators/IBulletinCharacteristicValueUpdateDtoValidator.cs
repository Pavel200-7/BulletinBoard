using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.ForValidating;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО обновления значения характеристики объявления по правилам:
/// Value:
///     1. Не пустая строка и не null.
///     2. Имеет длинну он 3 до 35.
///     3. Может хранить только русские, английские буквы нижнего, верхнего регистра и пробелы и цифры.
///     4. Является уникальным для выбранной характеристики.
/// </summary>
public interface IBulletinCharacteristicValueUpdateDtoValidator : IDtoValidator<BulletinCharacteristicValueUpdateDtoForValidating>
{
}
