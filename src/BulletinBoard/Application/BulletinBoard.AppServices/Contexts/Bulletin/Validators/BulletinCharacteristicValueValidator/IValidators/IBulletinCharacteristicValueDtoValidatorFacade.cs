using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.ForValidating;
using FluentValidation.Results;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinCharacteristicValue
/// </summary>
public interface IBulletinCharacteristicValueDtoValidatorFacade
{
    /// <summary>
    /// Вызов валидатора BulletinCharacteristicValueCreateDto
    /// </summary>
    public Task<ValidationResult> ValidateAsync(BulletinCharacteristicValueCreateDto entityDto);

    /// <summary>
    /// Вызов валидатора BulletinCharacteristicValueUpdateDtoForValidating
    /// </summary>
    public Task<ValidationResult> ValidateAsync(BulletinCharacteristicValueUpdateDtoForValidating entityDto);
}
