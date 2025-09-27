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
/// </summary>
public interface IBulletinImageCreateDtoValidator : IDtoValidator<BulletinImageCreateDto>
{
}
