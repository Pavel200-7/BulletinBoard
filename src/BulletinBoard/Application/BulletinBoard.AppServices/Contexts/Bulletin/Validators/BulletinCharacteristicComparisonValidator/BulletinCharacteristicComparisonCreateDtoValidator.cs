using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.CustomValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.CommonValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator;

/// <inheritdoc/>
public class BulletinCharacteristicComparisonCreateDtoValidator : AbstractValidator<BulletinCharacteristicComparisonCreateDto>, IBulletinCharacteristicComparisonCreateDtoValidator
{
    private readonly IBulletinMainRepository _bulletibRepository;
    private readonly IBulletinCharacteristicRepository _characteristicRepository;
    private readonly IBulletinCharacteristicValueRepository _characteristicValueRepository;
    private readonly IBulletinCharacteristicComparisonRepository _characteristicComparisonRepository;
    private readonly IBulletinCharacteristicComparisonSpecificationBuilder _characteristicComparisonSpecificationBuilder;

    /// <inheritdoc/>
    public BulletinCharacteristicComparisonCreateDtoValidator
        (
        IBulletinMainRepository bulletibRepository,
        IBulletinCharacteristicRepository characteristicRepository,
        IBulletinCharacteristicValueRepository characteristicValueRepository,
        IBulletinCharacteristicComparisonRepository characteristicComparisonRepository,
        IBulletinCharacteristicComparisonSpecificationBuilder characteristicComparisonSpecificationBuilder
        )
    {
        _bulletibRepository = bulletibRepository;
        _characteristicRepository = characteristicRepository;
        _characteristicValueRepository = characteristicValueRepository;
        _characteristicComparisonRepository = characteristicComparisonRepository;
        _characteristicComparisonSpecificationBuilder = characteristicComparisonSpecificationBuilder;


    RuleFor(createDto => createDto.BulletinId)
            .SetAsyncValidator(new BulletinIdValidator<BulletinCharacteristicComparisonCreateDto>(_bulletibRepository));

        RuleFor(createDto => createDto.CharacteristicId)
            .MustAsync(async (dto, id, validationContext, cancellationToken) =>
            {
                Guid bulletinId = dto.BulletinId;
                var validator = new CharacteristicIdValidator<BulletinCharacteristicComparisonCreateDto>(
                    bulletinId, _bulletibRepository, _characteristicRepository, _characteristicComparisonRepository, _characteristicComparisonSpecificationBuilder);
                return await validator.IsValidAsync(validationContext, id, cancellationToken);
            }).WithMessage("{Error}");

        RuleFor(createDto => createDto.CharacteristicValueId)
            .MustAsync(async (dto, id, validationContext, cancellationToken) =>
            {
                Guid characteristicId = dto.CharacteristicId;
                var validator = new CharacteristicValueIdValidator<BulletinCharacteristicComparisonCreateDto>(
                    characteristicId, _characteristicValueRepository);
                return await validator.IsValidAsync(validationContext, id, cancellationToken);
            }).WithMessage("{Error}");
    }
}
