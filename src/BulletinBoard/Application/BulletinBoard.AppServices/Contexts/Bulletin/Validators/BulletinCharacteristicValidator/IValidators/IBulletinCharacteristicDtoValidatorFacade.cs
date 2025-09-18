using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.ForValidating;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinCharacteristic
/// </summary>
public interface IBulletinCharacteristicDtoValidatorFacade
{
    /// <summary>
    /// Вызов валидатора BulletinCharacteristicCreateDto
    /// </summary>
    public Task<ValidationResult> ValidateAsync(BulletinCharacteristicCreateDto entityDto);

    /// <summary>
    /// Вызов валидатора BulletinCharacteristicUpdateDtoForValidating
    /// </summary>
    public Task<ValidationResult> ValidateAsync(BulletinCharacteristicUpdateDtoForValidating entityDto);

}
