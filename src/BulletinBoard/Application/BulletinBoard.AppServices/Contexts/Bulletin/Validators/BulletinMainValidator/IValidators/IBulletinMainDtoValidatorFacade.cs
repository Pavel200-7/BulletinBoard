using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinMain
/// </summary>
public interface IBulletinMainDtoValidatorFacade : IValidatorFacade<BulletinMainCreateDto, BulletinMainUpdateDto>
{
}
