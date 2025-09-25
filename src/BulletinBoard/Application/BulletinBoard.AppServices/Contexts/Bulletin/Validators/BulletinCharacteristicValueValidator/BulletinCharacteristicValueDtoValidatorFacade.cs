using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.UpdateDto;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator;

/// <inheritdoc/>
public class BulletinCharacteristicValueDtoValidatorFacade : BaseValidatorFacade
    <
    BulletinCharacteristicValueCreateDto,
    BulletinCharacteristicValueUpdateDtoForValidating,
    IBulletinCharacteristicValueCreateDtoValidator,
    IBulletinCharacteristicValueUpdateDtoValidator,
    IBulletinCharacteristicValueDeleteValidator
    >, IBulletinCharacteristicValueDtoValidatorFacade
{
    /// <inheritdoc/>
    public BulletinCharacteristicValueDtoValidatorFacade
        (
        IBulletinCharacteristicValueCreateDtoValidator createDtoValidator,
        IBulletinCharacteristicValueUpdateDtoValidator updateDtoValidator,
        IBulletinCharacteristicValueDeleteValidator deleteValidator
        ) : base(createDtoValidator, updateDtoValidator, deleteValidator)
    {
    }
}
