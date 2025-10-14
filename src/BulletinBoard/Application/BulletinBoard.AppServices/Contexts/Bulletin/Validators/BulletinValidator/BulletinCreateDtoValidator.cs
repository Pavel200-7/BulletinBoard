using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.CustomValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.CommonValidators;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.CreateDto;
using FluentValidation;
using System.Net;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator;

/// <inheritdoc/>
public class BulletinCreateDtoValidator : AbstractValidator<BulletinCreateDto>, IBulletinCreateDtoValidator
{
    private readonly IBulletinCharacteristicValueRepository _characteristicValueRepository;
    private IBulletinCharacteristicRepository _characteristicRepository;

    /// <inheritdoc/>
    public BulletinCreateDtoValidator
        (
        IValidator<BulletinMainCreateDto> mainValidator,
        IBulletinCharacteristicRepository characteristicRepository,
        IBulletinCharacteristicValueRepository characteristicValueRepository
        )
    {
        _characteristicRepository = characteristicRepository;
        _characteristicValueRepository = characteristicValueRepository;

        RuleFor(createDto => createDto.BulletinMain)
            .SetValidator(mainValidator);

        RuleForEach(createDto => createDto.CharacteristicComparisons)
            .MustAsync(async (dto, characteristicComparison, validationContext, cancellationToken) =>
            {
                Guid bulletinCategoryId = dto.BulletinMain.CategoryId;
                Guid characteristicId = characteristicComparison.CharacteristicId;

                var validator = new CharacteristicIdValidator<BulletinCreateDto>(
                    bulletinCategoryId, _characteristicRepository);

                return await validator.IsValidAsync(validationContext, characteristicId, cancellationToken);
            }).WithMessage("{Error}")
            .MustAsync(async (dto, characteristicComparison, validationContext, cancellationToken) =>
            {
                Guid characteristicId = characteristicComparison.CharacteristicId;
                Guid characteristicValueId = characteristicComparison.CharacteristicValueId;

                var validator = new CharacteristicValueIdValidator<BulletinCreateDto>(
                    characteristicId, _characteristicValueRepository);

                return await validator.IsValidAsync(validationContext, characteristicValueId, cancellationToken);
            }).WithMessage("{Error}");

        RuleForEach(createDto => createDto.Images)
            .NotNull();
    }
}
