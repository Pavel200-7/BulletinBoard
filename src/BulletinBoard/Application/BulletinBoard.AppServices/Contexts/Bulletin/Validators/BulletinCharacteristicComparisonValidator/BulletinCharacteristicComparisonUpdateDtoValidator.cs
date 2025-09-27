using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.CommonValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.UpdateDto;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator;

/// <inheritdoc/>
public class BulletinCharacteristicComparisonUpdateDtoValidator : AbstractValidator<BulletinCharacteristicComparisonUpdateDtoForValidating>, IBulletinCharacteristicComparisonUpdateDtoValidator
{
    private readonly IBulletinCharacteristicValueRepository _characteristicValueRepository;

    /// <inheritdoc/>
    public BulletinCharacteristicComparisonUpdateDtoValidator
        (
        IBulletinCharacteristicValueRepository characteristicValueRepository
        )
    {
        _characteristicValueRepository = characteristicValueRepository;

        RuleFor(updateDto => updateDto.CharacteristicValueId)
            .MustAsync(async (dto, id, validationContext, cancellationToken) =>
            {
                Guid characteristicId = dto.CharacteristicId;
                var validator = new CharacteristicValueIdValidator<BulletinCharacteristicComparisonUpdateDtoForValidating>(
                    characteristicId,
                    _characteristicValueRepository
                    );
                return await validator.IsValidAsync(validationContext, id, cancellationToken);
            }).WithMessage("{Error}");
    }
}
