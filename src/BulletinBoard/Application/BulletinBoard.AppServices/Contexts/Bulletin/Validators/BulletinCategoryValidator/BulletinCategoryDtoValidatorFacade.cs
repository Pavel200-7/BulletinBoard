using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.ForValidating;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator;

/// <inheritdoc/>
public sealed class BulletinCategoryDtoValidatorFacade : BaseValidatorFacade
    <
    BulletinCategoryCreateDto,
    BulletinCategoryUpdateDtoForValidating,
    IBulletinCategoryCreateDtoValidator,
    IBulletinCategoryUpdateDtoValidator,
    IBulletinCategoryDeleteValidator
    >, 
    IBulletinCategoryDtoValidatorFacade
{
    /// <inheritdoc/>
    public BulletinCategoryDtoValidatorFacade
        (
        IBulletinCategoryCreateDtoValidator createDtoValidator, 
        IBulletinCategoryUpdateDtoValidator updateDtoValidator, 
        IBulletinCategoryDeleteValidator deleteValidator
        ) : base(createDtoValidator, updateDtoValidator, deleteValidator)
    {
    }
}
