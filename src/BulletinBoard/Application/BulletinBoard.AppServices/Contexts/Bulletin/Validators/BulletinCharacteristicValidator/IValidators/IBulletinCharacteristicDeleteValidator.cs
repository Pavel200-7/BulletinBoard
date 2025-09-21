using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;

/// <summary>
/// Данный валидатор принимает id удаляемой сущности BulletinCharacteristic 
/// и проверяет, есть ли зависимые от нее записи сущностей:
///     1. BulletinCharacteristicValue (дочерние категории).
///     2. BulletinCharacteristicComparison (связи характеристик и объявлений).
/// </summary>
public interface IBulletinCharacteristicDeleteValidator
{
    /// <summary>
    /// Стандартизированный метод валидации.
    /// </summary>
    /// <param name="entityId">id характеристики на удаление.</param>
    /// <param name="cancellation">Токен отмены, ставится автоматически.</param>
    /// <returns>Результат валидации.</returns>
    public Task<ValidationResult> ValidateAsync(Guid entityId, CancellationToken cancellation = default);
}
