using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;

/// <summary>
/// Данный валидатор принимает id удаляемой сущности BulletinCategory 
/// и проверяет, есть ли зависимые от нее записи сущностей:
///     1. BulletinCategory (дочерние категории).
///     2. Bulletin Main (объявления).
///     3. BulletinCharacteristic (характеристики объявлений)
/// </summary>
public interface IBulletinCategoryDeleteValidator : IDeleteValidator
{
}
