using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО создания изображения объявления по правилам:
/// Id:
///     1. Не null.
/// BulletinId:
///     1. Существует.
///     2. Не заблокировано (объявление).
/// Name:
///     1. Не пустая строка и не null.
///     2. Имеет длинну он 3 до 255.
///     3. Может хранить только русские, английские буквы нижнего, верхнего регистра, пробелы и знаки пунктуации.
/// </summary>
public interface IBulletinImageCreateDtoValidator : IDtoValidator<BulletinImageCreateDto>
{
}
