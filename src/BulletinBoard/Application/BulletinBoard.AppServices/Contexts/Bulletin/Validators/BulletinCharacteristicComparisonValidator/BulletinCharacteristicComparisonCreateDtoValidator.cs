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

    /// <inheritdoc/>
    public BulletinCharacteristicComparisonCreateDtoValidator
        (
        IBulletinMainRepository bulletibRepository
        )
    {
        _bulletibRepository = bulletibRepository;


        RuleFor(bulletinCharacteristicComparisonCreateDto => bulletinCharacteristicComparisonCreateDto.BulletinId)
            .SetAsyncValidator(new BulletinIdValidator<BulletinCharacteristicComparisonCreateDto>(_bulletibRepository));
    }
}
