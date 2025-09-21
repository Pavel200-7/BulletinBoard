using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;

/// <summary>
/// Данный валидатор принимает id удаляемой сущности BulletinCharacteristicComparison
/// и проверяет, есть ли зависимые от нее записи сущностей:
///     1. Строгих зависимостей пока нет.
/// </summary>
public interface IBulletinCharacteristicComparisonDeleteValidator : IDeleteValidator
{
}
