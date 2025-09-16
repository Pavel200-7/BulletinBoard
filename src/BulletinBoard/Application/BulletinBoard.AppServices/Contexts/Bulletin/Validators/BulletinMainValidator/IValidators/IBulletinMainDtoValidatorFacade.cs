using FluentValidation.Results;
using BulletinBoard.Contracts.Bulletin.BelletinMain;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinMain
/// </summary>
public interface IBulletinMainDtoValidatorFacade
{
    /// <summary>
    /// Вызов валидатора BulletinMainCreateDto
    /// </summary>
    public Task<ValidationResult> ValidateAsync(BulletinMainCreateDto entityDto);

    /// <summary>
    /// Вызов валидатора BulletinMainUpdateDto
    /// </summary>
    public Task<ValidationResult> ValidateAsync(BulletinMainUpdateDto entityDto);
}
