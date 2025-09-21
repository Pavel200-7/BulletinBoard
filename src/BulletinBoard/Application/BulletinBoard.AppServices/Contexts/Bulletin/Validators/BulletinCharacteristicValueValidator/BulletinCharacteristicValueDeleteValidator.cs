using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator;

/// <inheritdoc/>
public class BulletinCharacteristicValueDeleteValidator : AbstractValidator<Guid>, IBulletinCharacteristicValueDeleteValidator
{
    private readonly IBulletinCharacteristicComparisonRepository _characteristicComparisonRepository;
    private readonly IBulletinCharacteristicComparisonSpecificationBuilder _characteristicComparisonSpecificationBuilder;

    /// <inheritdoc/>
    public BulletinCharacteristicValueDeleteValidator
        (
        IBulletinCharacteristicComparisonRepository characteristicComparisonRepository,
        IBulletinCharacteristicComparisonSpecificationBuilder characteristicComparisonSpecificationBuilder
        )
    {
        _characteristicComparisonRepository = characteristicComparisonRepository;
        _characteristicComparisonSpecificationBuilder = characteristicComparisonSpecificationBuilder;

        RuleFor(id => id)
            .MustAsync(async (id, idField, validationContext, cancellationToken) =>
            {
                if (await IsHaveDependentCharacteristicComporations(id)) { return false; }
                return true;
            }).WithMessage("This characteristic value can not be deleted because is has dependent characteristics comporations (bulletins)");
    }

    /// <summary>
    /// Есть ли зависящии связи характеристик (объявеления).
    /// </summary>
    /// <returns></returns>
    private async Task<bool> IsHaveDependentCharacteristicComporations(Guid characteristicValueId)
    {
        var specification = _characteristicComparisonSpecificationBuilder
            .WhereCharacteristicValueId(characteristicValueId)
            .Paginate(1, 1)
            .Build();
        IReadOnlyCollection<BulletinCharacteristicComparisonDto> dependentCharacteristicComparisons = await _characteristicComparisonRepository.FindAsync(specification);
        return dependentCharacteristicComparisons.Count != 0;
    }

}
