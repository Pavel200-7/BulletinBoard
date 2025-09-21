using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;

/// <summary>
///  Интерфейс валидатора.
/// </summary>
public interface IDtoValidator<TDto> where TDto : class
{
    /// <summary>
    /// Стандартизированный метод валидации.
    /// </summary>
    /// <param name="entityDto">ДТО сущности.</param>
    /// <param name="cancellation">Токен отмены, ставится автоматически.</param>
    /// <returns>Результат валидации.</returns>
    public Task<ValidationResult> ValidateAsync(TDto entityDto, CancellationToken cancellation = default);
}
