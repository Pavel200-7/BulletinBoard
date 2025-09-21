using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;

/// <summary>
/// Валидатор, проверяющий сущность перед ее удалением.
/// </summary>
public interface IDeleteValidator
{
    /// <summary>
    /// Стандартизированный метод валидации.
    /// </summary>
    /// <param name="entityId">id сущности на удаление.</param>
    /// <param name="cancellation">Токен отмены, ставится автоматически.</param>
    /// <returns>Результат валидации.</returns>
    public Task<ValidationResult> ValidateAsync(Guid entityId, CancellationToken cancellation = default);

}
