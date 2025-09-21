using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.ForValidating;
using FluentValidation;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;


/// <summary>
/// Валидатор проводящий проверку ДТО обновления характеристики по правилам:
/// Name:
///     1. Не пустая строка и не null.
///     2. Имеет длинну он 3 до 35.
///     3. Может хранить только русские, английские буквы нижнего, верхнего регистра и пробелы и цифры.
///     4. Является уникальным для выбранной категории.
/// </summary>
public interface IBulletinCharacteristicUpdateDtoValidator : IDtoValidator<BulletinCharacteristicUpdateDtoForValidating>
{
}
