using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using FluentValidation.Results;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator;

/// <inheritdoc/>
public class BulletinMainDtoValidatorFacade : IBulletinMainDtoValidatorFacade
{
    private readonly IBulletinMainCreateDtoValidator _createDtoValidator;
    private readonly IBulletinMainUpdateDtoValidator _updateDtoValidator;

    /// <inheritdoc/>
    public BulletinMainDtoValidatorFacade
        (
            IBulletinMainCreateDtoValidator createDtoValidator,
            IBulletinMainUpdateDtoValidator updateDtoValidator
        )
    {
        _createDtoValidator = createDtoValidator;
        _updateDtoValidator = updateDtoValidator;
    }

    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateAsync(BulletinMainCreateDto entityDto)
    {
        return await _createDtoValidator.ValidateAsync(entityDto);
    }

    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateAsync(BulletinMainUpdateDto entityDto)
    {
        return await _updateDtoValidator.ValidateAsync(entityDto);
    }
}
