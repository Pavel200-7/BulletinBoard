

using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator.IValidators;

/// <summary>
/// Данный валидатор принимает id удаляемой сущности BulletinUser
/// и проверяет, есть ли зависимые от нее записи сущностей:
///     1. BulletinMain (объявления).
/// </summary>
public interface IBulletinUserDeleteValidator : IDeleteValidator
{
}
