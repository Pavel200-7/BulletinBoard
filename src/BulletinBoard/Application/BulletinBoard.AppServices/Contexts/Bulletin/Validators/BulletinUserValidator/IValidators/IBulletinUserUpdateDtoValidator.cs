using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Contracts.Bulletin.BulletinUser.UpdateDto;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО обновления пользователя по правилам:
///     1. Валидация происходит в домене, который является основным для данной сущности.
/// </summary>
public interface IBulletinUserUpdateDtoValidator : IDtoValidator<BulletinUserUpdateDtoForValidating>
{
}
