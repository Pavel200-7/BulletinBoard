using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BulletinUser;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО создания пользователя по правилам:
///     1. Валидация происходит в домене, который является основным для данной сущности.
/// </summary>
public interface IBulletinUserCreateDtoValidator : IDtoValidator<BulletinUserCreateDto>
{
    // Валидации нет. (не предполагается текущими требованиями)
}
