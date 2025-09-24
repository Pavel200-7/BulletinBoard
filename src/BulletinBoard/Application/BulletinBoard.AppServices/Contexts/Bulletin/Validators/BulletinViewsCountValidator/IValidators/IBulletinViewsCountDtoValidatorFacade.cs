using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.ForValidating;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinViewsCount
/// </summary>
public interface IBulletinViewsCountDtoValidatorFacade : IValidatorFacade<BulletinViewsCountCreateDto, BulletinViewsCountUpdateDtoForValidating>
{
}
