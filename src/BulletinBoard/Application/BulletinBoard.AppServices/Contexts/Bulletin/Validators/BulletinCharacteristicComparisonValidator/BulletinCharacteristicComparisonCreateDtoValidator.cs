using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.CustomValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator;

/// <inheritdoc/>
public class BulletinCharacteristicComparisonCreateDtoValidator : AbstractValidator<BulletinCharacteristicComparisonCreateDto>, IBulletinCharacteristicComparisonCreateDtoValidator
{
    private readonly IBulletinMainRepository _bulletibRepository;
    private readonly IBulletinCharacteristicRepository _characteristicRepository;
    private readonly IBulletinCharacteristicValueRepository _characteristicValueRepository;

    /// <inheritdoc/>
    public BulletinCharacteristicComparisonCreateDtoValidator
        (
        IBulletinMainRepository bulletibRepository,
        IBulletinCharacteristicRepository characteristicRepository,
        IBulletinCharacteristicValueRepository characteristicValueRepository
        )
    {
        _bulletibRepository = bulletibRepository;
        _characteristicRepository = characteristicRepository;
        _characteristicValueRepository = characteristicValueRepository;


        RuleFor(bulletinCharacteristicComparisonCreateDto => bulletinCharacteristicComparisonCreateDto.BulletinId)
            .SetAsyncValidator(new BulletinIdValidator<BulletinCharacteristicComparisonCreateDto>(_bulletibRepository));

        RuleFor(bulletinCharacteristicComparisonCreateDto => bulletinCharacteristicComparisonCreateDto.CharacteristicId)
            .MustAsync(async (dto, id, validationContext, cancellationToken) =>
            {
                Guid bulletinId = dto.BulletinId;
                var validator = new CharacteristicIdValidator<BulletinCharacteristicComparisonCreateDto>(
                    bulletinId,
                    _bulletibRepository,
                    _characteristicRepository
                    );
                return await validator.IsValidAsync(validationContext, id, cancellationToken);
            }).WithMessage("{Error}");

        RuleFor(bulletinCharacteristicComparisonCreateDto => bulletinCharacteristicComparisonCreateDto.CharacteristicValueId)
            .MustAsync(async (dto, id, validationContext, cancellationToken) =>
            {
                Guid characteristicId = dto.CharacteristicId;
                var validator = new CharacteristicValueIdValidator<BulletinCharacteristicComparisonCreateDto>(
                    characteristicId,
                    _characteristicValueRepository
                    );
                return await validator.IsValidAsync(validationContext, id, cancellationToken);
            }).WithMessage("{Error}");
    }
}
