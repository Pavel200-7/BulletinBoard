using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinViewsCount
/// </summary>
public interface IBulletinViewsCountDtoValidatorFacade : IValidatorFacade<BulletinViewsCountCreateDto, BulletinViewsCountUpdateDtoForValidating>
{
}
