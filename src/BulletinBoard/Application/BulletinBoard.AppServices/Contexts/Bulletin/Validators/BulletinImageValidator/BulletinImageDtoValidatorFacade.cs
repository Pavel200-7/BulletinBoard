using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator;

/// <inheritdoc/>
public class BulletinImageDtoValidatorFacade : BaseValidatorFacade
    <
    BulletinImageCreateDto,
    BulletinImageUpdateDtoForValidating,
    IBulletinImageCreateDtoValidator,
    IBulletinImageUpdateDtoValidator,
    IBulletinImageDeleteValidator
    >, IBulletinImageDtoValidatorFacade
{
    /// <inheritdoc/>
    public BulletinImageDtoValidatorFacade
        (
        IBulletinImageCreateDtoValidator bulletinCategoryCreateDtoValidator,
        IBulletinImageUpdateDtoValidator bulletinCategoryUpdateDtoValidator,
        IBulletinImageDeleteValidator deleteValidator
        ) : base(bulletinCategoryCreateDtoValidator, bulletinCategoryUpdateDtoValidator, deleteValidator)
    {
    }
}
