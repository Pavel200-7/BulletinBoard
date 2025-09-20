using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;

/// <summary>
/// Данный валидатор принимает id удаляемой сущности BulletinCategory 
/// и проверяет, есть ли зависимые от нее записи сущностей:
///     1. BulletinCategory (дочерние категории).
///     2. Bulletin Main (объявления).
///     3. BulletinCharacteristic (характеристики объявлений)
/// </summary>
public interface IBulletinCategoryDeleteValidator
{
    /// <summary>
    /// Стандартизированный метод валидации.
    /// </summary>
    /// <param name="entityId">id категории на удаление.</param>
    /// <param name="cancellation">Токен отмены, ставится автоматически.</param>
    /// <returns>Результат валидации.</returns>
    public Task<ValidationResult> ValidateAsync(Guid entityId, CancellationToken cancellation = default);
}
