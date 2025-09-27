using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinImage.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО обновления изображения объявления по правилам:
///     1. нет
/// </summary>
public interface IBulletinImageUpdateDtoValidator : IDtoValidator<BulletinImageUpdateDtoForValidating>
{
}
