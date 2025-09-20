using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinCategory
/// </summary>
public interface IBulletinCategoryDtoValidatorFacade
{
    /// <summary>
    /// Вызов валидатора BulletinCategoryCreateDto
    /// </summary>
    public Task<ValidationResult> ValidateAsync(BulletinCategoryCreateDto entityDto);

    /// <summary>
    /// Вызов валидатора BulletinCategoryUpdateDto
    /// </summary>
    public Task<ValidationResult> ValidateAsync(BulletinCategoryUpdateDto entityDto);

    /// <summary>
    /// Вызов валидатора самой категории перед ее удалением.
    /// </summary>
    /// <param name="entityId">id категории</param>
    /// <returns></returns>
    public Task<ValidationResult> ValidateBeforeDeletingAsync(Guid entityId);
}
