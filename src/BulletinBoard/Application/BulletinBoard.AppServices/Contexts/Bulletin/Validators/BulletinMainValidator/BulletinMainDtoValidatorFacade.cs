using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BelletinMain.ForValidating;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.ForValidating;
using FluentValidation.Results;

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
