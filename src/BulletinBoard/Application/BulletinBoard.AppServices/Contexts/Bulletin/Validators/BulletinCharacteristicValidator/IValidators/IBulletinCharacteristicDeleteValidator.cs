using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;

/// <summary>
/// Данный валидатор принимает id удаляемой сущности BulletinCharacteristic 
/// и проверяет, есть ли зависимые от нее записи сущностей:
///     1. BulletinCharacteristicComparison (связи характеристик и объявлений).
/// </summary>
public interface IBulletinCharacteristicDeleteValidator : IDeleteValidator
{
}
