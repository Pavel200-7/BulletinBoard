using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator;

/// <inheritdoc/>
public class BulletinCharacteristicComparisonUpdateDtoValidator : AbstractValidator<BulletinCharacteristicComparisonUpdateDto>, IBulletinCharacteristicComparisonUpdateDtoValidator
{
    /// <inheritdoc/>
    public BulletinCharacteristicComparisonUpdateDtoValidator()
    {
    }
}
