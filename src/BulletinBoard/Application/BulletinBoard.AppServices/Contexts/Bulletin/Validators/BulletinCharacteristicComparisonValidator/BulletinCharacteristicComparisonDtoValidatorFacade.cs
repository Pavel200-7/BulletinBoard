using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator;

/// <inheritdoc/>
public class BulletinCharacteristicComparisonDtoValidatorFacade : BaseValidatorFacade
    <
    BulletinCharacteristicComparisonCreateDto,
    BulletinCharacteristicComparisonUpdateDtoForValidating,
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
