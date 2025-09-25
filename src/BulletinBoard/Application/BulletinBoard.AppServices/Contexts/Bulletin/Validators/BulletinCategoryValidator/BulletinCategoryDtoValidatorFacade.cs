using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.UpdateDto;


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
