using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage.UpdateDto;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator.IValidators;

/// <summary>
/// /// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinImage
/// </summary>
public interface IBulletinImageDtoValidatorFacade : IValidatorFacade<BulletinImageCreateDto, BulletinImageUpdateDtoForValidating>
{
}
