using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator;

/// <inheritdoc/>
public class BulletinViewsCountDtoValidatorFacade : BaseValidatorFacade
    <
    BulletinViewsCountCreateDto,
    BulletinViewsCountUpdateDtoForValidating,
    IBulletinViewsCountCreateDtoValidator,
    IBulletinViewsCountUpdateDtoValidator,
    IBulletinViewsCountDeleteValidator
    >, IBulletinViewsCountDtoValidatorFacade
{
    /// <inheritdoc/>
    public BulletinViewsCountDtoValidatorFacade
        (
        IBulletinViewsCountCreateDtoValidator bulletinCategoryCreateDtoValidator, 
        IBulletinViewsCountUpdateDtoValidator bulletinCategoryUpdateDtoValidator, 
        IBulletinViewsCountDeleteValidator deleteValidator
        ) : base(bulletinCategoryCreateDtoValidator, bulletinCategoryUpdateDtoValidator, deleteValidator)
    {
    }
}
