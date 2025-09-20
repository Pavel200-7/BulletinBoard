using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.ForValidating;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator;

/// <inheritdoc/>
public class BulletinCharacteristicValueDtoValidatorFacade : IBulletinCharacteristicValueDtoValidatorFacade
{
    private readonly IBulletinCharacteristicValueCreateDtoValidator _createDtoValidator;
    private readonly IBulletinCharacteristicValueUpdateDtoValidator _updateDtoValidator;

    /// <inheritdoc/>
    public BulletinCharacteristicValueDtoValidatorFacade
        (
        IBulletinCharacteristicValueCreateDtoValidator createDtoValidator,
        IBulletinCharacteristicValueUpdateDtoValidator updateDtoValidator
        )
    {
        _createDtoValidator = createDtoValidator;
        _updateDtoValidator = updateDtoValidator;
    }

    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateAsync(BulletinCharacteristicValueCreateDto entityDto)
    {
        return await _createDtoValidator.ValidateAsync(entityDto);
    }

    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateAsync(BulletinCharacteristicValueUpdateDtoForValidating entityDto)
    {
        return await _updateDtoValidator.ValidateAsync(entityDto);
    }
}
