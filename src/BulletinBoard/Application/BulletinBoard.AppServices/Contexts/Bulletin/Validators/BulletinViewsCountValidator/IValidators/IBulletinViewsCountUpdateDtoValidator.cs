using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.ForValidating;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО обновления счетчика просмотров по правилам:
/// ViewsCount:
///     1. Неотрицательное целое число.
/// </summary>
public interface IBulletinViewsCountUpdateDtoValidator : IDtoValidator<BulletinViewsCountUpdateDtoForValidating>
{
}
