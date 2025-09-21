using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.ForValidating;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator;

/// <inheritdoc/>
public class BulletinCharacteristicDtoValidatorFacade : BaseValidatorFacade
    <
    BulletinCharacteristicCreateDto,
    BulletinCharacteristicUpdateDtoForValidating,
    IBulletinCharacteristicCreateDtoValidator,
    IBulletinCharacteristicUpdateDtoValidator,
    IBulletinCharacteristicDeleteValidator
    >, 
    IBulletinCharacteristicDtoValidatorFacade
{
    /// <inheritdoc/>
    public BulletinCharacteristicDtoValidatorFacade
        (
            IBulletinCharacteristicCreateDtoValidator createDtoValidator,
            IBulletinCharacteristicUpdateDtoValidator updateDtoValidator,
            IBulletinCharacteristicDeleteValidator deleteValidator
        ) : base(createDtoValidator, updateDtoValidator, deleteValidator)
    {
    }
}
