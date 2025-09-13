using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator;

/// <inheritdoc/>
public sealed class BulletinCategoryDtoValidatorFacade : IBulletinCategoryDtoValidatorFacade
{
    private readonly IBulletinCategoryCreateDtoValidator _bulletinCategoryCreateDtoValidator;
    private readonly IBulletinCategoryUpdateDtoValidator _bulletinCategoryUpdateDtoValidator;

    /// <inheritdoc/>
    public BulletinCategoryDtoValidatorFacade
        (
            IBulletinCategoryCreateDtoValidator bulletinCategoryCreateDtoValidator, 
            IBulletinCategoryUpdateDtoValidator bulletinCategoryUpdateDtoValidator
        )
    {
        _bulletinCategoryCreateDtoValidator = bulletinCategoryCreateDtoValidator;
        _bulletinCategoryUpdateDtoValidator = bulletinCategoryUpdateDtoValidator;
    }

    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateAsync(BulletinCategoryCreateDto entityDto)
    {
        return await _bulletinCategoryCreateDtoValidator.ValidateAsync(entityDto);
    }

    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateAsync(BulletinCategoryUpdateDto entityDto)
    {
        return await _bulletinCategoryUpdateDtoValidator.ValidateAsync(entityDto);
    }
}
