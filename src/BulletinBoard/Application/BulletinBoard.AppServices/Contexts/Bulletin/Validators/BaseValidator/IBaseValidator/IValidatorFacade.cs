using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации сущности в ее разных формах.
/// </summary>
public interface IValidatorFacade<TCreateDto, TUpdateDto>
    where TCreateDto : class
    where TUpdateDto : class
{
    /// <summary>
    /// Валидировать ДТО создания сущности.
    /// </summary>
    public Task<ValidationResult> ValidateAsync(TCreateDto entityDto);

    /// <summary>
    /// Валидировать ДТО обновления сущности.
    /// </summary>
    public Task<ValidationResult> ValidateAsync(TUpdateDto entityDto);

    /// <summary>
    /// Валидировать сущность по id до ее удаления.
    /// </summary>
    /// <param name="entityId">id сущности.</param>
    /// <returns></returns>
    public Task<ValidationResult> ValidateBeforeDeletingAsync(Guid entityId);
}
