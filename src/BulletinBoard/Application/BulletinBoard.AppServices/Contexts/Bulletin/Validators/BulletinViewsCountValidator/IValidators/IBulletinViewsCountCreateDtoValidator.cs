using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.CreateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО создания счетчика просмотров по правилам:
/// BulletinId:
///     1. Не null.
///     2. Существует.
///     3. Не заблокированно.
/// ViewsCount:
///     1. Равно 0.
/// </summary>
public interface IBulletinViewsCountCreateDtoValidator : IDtoValidator<BulletinViewsCountCreateDto>
{
}
