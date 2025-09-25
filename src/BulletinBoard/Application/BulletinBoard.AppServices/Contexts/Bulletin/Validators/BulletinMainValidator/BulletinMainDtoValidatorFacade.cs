using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BelletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BelletinMain.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator;

/// <inheritdoc/>
public class BulletinMainDtoValidatorFacade : BaseValidatorFacade
    <
    BulletinMainCreateDto,
    BulletinMainUpdateDtoForValidating,
    IBulletinMainCreateDtoValidator,
    IBulletinMainUpdateDtoValidator,
    IBulletinMainDeleteValidator
    >, IBulletinMainDtoValidatorFacade
{

    /// <inheritdoc/>
    public BulletinMainDtoValidatorFacade
        (
        IBulletinMainCreateDtoValidator createDtoValidator,
        IBulletinMainUpdateDtoValidator updateDtoValidator,
        IBulletinMainDeleteValidator deleteValidator
        ) : base(createDtoValidator, updateDtoValidator, deleteValidator)
    {
    }
}
