using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator;

/// <inheritdoc/>
public sealed class BulletinCategoryDtoValidatorFacade : IBulletinCategoryDtoValidatorFacade
{
    private readonly IBulletinCategoryCreateDtoValidator _createDtoValidator;
    private readonly IBulletinCategoryUpdateDtoValidator _updateDtoValidator;

    /// <inheritdoc/>
    public BulletinCategoryDtoValidatorFacade
        (
            IBulletinCategoryCreateDtoValidator bulletinCategoryCreateDtoValidator, 
            IBulletinCategoryUpdateDtoValidator bulletinCategoryUpdateDtoValidator
        )
    {
        _createDtoValidator = bulletinCategoryCreateDtoValidator;
        _updateDtoValidator = bulletinCategoryUpdateDtoValidator;
    }

    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateAsync(BulletinCategoryCreateDto entityDto)
    {
        return await _createDtoValidator.ValidateAsync(entityDto);
    }

    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateAsync(BulletinCategoryUpdateDto entityDto)
    {
        return await _updateDtoValidator.ValidateAsync(entityDto);
    }
}
