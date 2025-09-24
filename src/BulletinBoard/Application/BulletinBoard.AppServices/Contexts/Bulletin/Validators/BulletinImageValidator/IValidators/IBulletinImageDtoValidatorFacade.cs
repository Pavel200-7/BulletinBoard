using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;

using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Contracts.Bulletin.BulletinImage.ForValidating;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator.IValidators;

/// <summary>
/// /// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinImage
/// </summary>
public interface IBulletinImageDtoValidatorFacade : IValidatorFacade<BulletinImageCreateDto, BulletinImageUpdateDtoForValidating>
{
}
