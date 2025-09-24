using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BelletinMain.ForValidating;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinMain
/// </summary>
public interface IBulletinMainDtoValidatorFacade : IValidatorFacade<BulletinMainCreateDto, BulletinMainUpdateDtoForValidating>
{
}
