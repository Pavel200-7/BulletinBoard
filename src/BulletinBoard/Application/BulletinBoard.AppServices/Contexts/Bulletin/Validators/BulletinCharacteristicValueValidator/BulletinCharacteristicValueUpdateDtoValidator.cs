using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.CustomValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.UpdateDto;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator;

/// <inheritdoc/>
public class BulletinCharacteristicValueUpdateDtoValidator : AbstractValidator<BulletinCharacteristicValueUpdateDtoForValidating>, IBulletinCharacteristicValueUpdateDtoValidator
{
    private readonly IBulletinCharacteristicValueRepository _characteristicValueRepository;
    private readonly IBulletinCharacteristicValueSpecificationBuilder _characteristicValueSpecificationBuilder;

    /// <inheritdoc/>
    public BulletinCharacteristicValueUpdateDtoValidator
        (
        IBulletinCharacteristicRepository characteristicRepository,
        IBulletinCharacteristicValueRepository characteristicValueRepository,
        IBulletinCharacteristicValueSpecificationBuilder characteristicValueSpecificationBuilder
        )
    {
        _characteristicValueRepository = characteristicValueRepository;
        _characteristicValueSpecificationBuilder = characteristicValueSpecificationBuilder;

        RuleFor(updateDto => updateDto.Value)
            .NotEmpty()
            .Length(3, 35)
            .Matches("^[а-яА-Яa-zA-Z0-9\\s]+$")
                .WithMessage("{PropertyName} can contain only letters, digits, and spaces")
            .MustAsync(async (dto, value, cancellation) =>
                await BullerinCharacteristicValueValidator.IsValueUniqueForCharacteristicAsync(
                    _characteristicValueRepository,
                    _characteristicValueSpecificationBuilder,
                    dto.CharacteristicId,
                    value))
                .WithMessage("This value is not unique for this characteristic.");
    }
}
