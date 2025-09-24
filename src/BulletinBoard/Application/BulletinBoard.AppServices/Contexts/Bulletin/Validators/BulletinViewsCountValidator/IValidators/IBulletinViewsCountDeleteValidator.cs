using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator.IValidators;

/// <summary>
/// Данный валидатор принимает id удаляемой сущности BulletinViewsCount
/// и проверяет, есть ли зависимые от нее записи сущностей:
///     1. Строгих зависимостей пока нет.
/// </summary>
public interface IBulletinViewsCountDeleteValidator : IDeleteValidator
{
}
