using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator;

/// <inheritdoc/>
public sealed class BulletinCategoryDtoValidatorFacade : IBulletinCategoryDtoValidatorFacade
{
    private readonly IBulletinCategoryCreateDtoValidator _createDtoValidator;
    private readonly IBulletinCategoryUpdateDtoValidator _updateDtoValidator;
    private readonly IBulletinCategoryDeleteValidator _deleteValidator;


    /// <inheritdoc/>
    public BulletinCategoryDtoValidatorFacade
        (
        IBulletinCategoryCreateDtoValidator bulletinCategoryCreateDtoValidator, 
        IBulletinCategoryUpdateDtoValidator bulletinCategoryUpdateDtoValidator,
        IBulletinCategoryDeleteValidator deleteValidator
        )
    {
        _createDtoValidator = bulletinCategoryCreateDtoValidator;
        _updateDtoValidator = bulletinCategoryUpdateDtoValidator;
        _deleteValidator = deleteValidator;
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

    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateBeforeDeletingAsync(Guid entityId)
    {
        return await _deleteValidator.ValidateAsync(entityId);
    }
}
