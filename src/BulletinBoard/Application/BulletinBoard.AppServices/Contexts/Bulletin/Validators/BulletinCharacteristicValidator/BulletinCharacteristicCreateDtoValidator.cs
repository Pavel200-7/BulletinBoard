using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.CustomValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator;

/// <inheritdoc/>
public class BulletinCharacteristicCreateDtoValidator : AbstractValidator<BulletinCharacteristicCreateDto>, IBulletinCharacteristicCreateDtoValidator
{
    private readonly IBulletinCharacteristicRepository _characteristicRepository;
    private readonly IBulletinCategoryRepository _categoryRepository;
    private readonly IBulletinCharacteristicSpecificationBuilder _specificationBuilder;

    /// <inheritdoc/>
    public BulletinCharacteristicCreateDtoValidator
        (
            IBulletinCharacteristicRepository characteristicRepository,
            IBulletinCategoryRepository categoryRepository,
            IBulletinCharacteristicSpecificationBuilder specificationBuilder
        )
    {
        _characteristicRepository = characteristicRepository;
        _categoryRepository = categoryRepository;
        _specificationBuilder = specificationBuilder;

        RuleFor(bulletinCharacteristicCreateDto => bulletinCharacteristicCreateDto.CategoryId)
            .NotEmpty()
            .SetAsyncValidator(new BulletinCategoryValidator<BulletinCharacteristicCreateDto>(_categoryRepository));

        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 35)
            .Matches("^[а-яА-Яa-zA-Z0-9\\s]+$")
                .WithMessage("{PropertyName} can contain only letters, digits, and spaces")
            .MustAsync(async (dto, name, cancellation) =>
                await BullerinCharacteristicNameValidator.IsNameUniqueForCategoryAsync(
                    _characteristicRepository,
                    _specificationBuilder,
                    dto.CategoryId,
                    name))
                .WithMessage("This name is not unique for this category.");
    }
}
