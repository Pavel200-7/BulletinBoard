using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinCategory
/// </summary>
public interface IBulletinCategoryDtoValidatorFacade : IValidatorFacade<BulletinCategoryCreateDto, BulletinCategoryUpdateDto>
{
}
