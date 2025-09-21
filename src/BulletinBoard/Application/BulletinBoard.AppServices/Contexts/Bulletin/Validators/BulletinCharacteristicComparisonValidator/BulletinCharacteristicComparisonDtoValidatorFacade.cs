using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator;

/// <inheritdoc/>
public class BulletinCharacteristicComparisonDtoValidatorFacade : BaseValidatorFacade
    <
    BulletinCharacteristicComparisonCreateDto,
    BulletinCharacteristicComparisonUpdateDto,
    IBulletinCharacteristicComparisonCreateDtoValidator,
    IBulletinCharacteristicComparisonUpdateDtoValidator,
    IBulletinCharacteristicComparisonDeleteValidator
    >, IBulletinCharacteristicComparisonDtoValidatorFacade
{
    /// <inheritdoc/>
    public BulletinCharacteristicComparisonDtoValidatorFacade
        (
        IBulletinCharacteristicComparisonCreateDtoValidator bulletinCategoryCreateDtoValidator,
        IBulletinCharacteristicComparisonUpdateDtoValidator bulletinCategoryUpdateDtoValidator,
        IBulletinCharacteristicComparisonDeleteValidator deleteValidator
        ) : base(bulletinCategoryCreateDtoValidator, bulletinCategoryUpdateDtoValidator, deleteValidator)
    {
    }
}
