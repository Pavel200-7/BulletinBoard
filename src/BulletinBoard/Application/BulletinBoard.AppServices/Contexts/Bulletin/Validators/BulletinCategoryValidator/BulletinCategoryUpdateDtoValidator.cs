using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.ForValidating;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator;

/// <inheritdoc/>
public sealed class BulletinCategoryUpdateDtoValidator : AbstractValidator<BulletinCategoryUpdateDtoForValidating>, IBulletinCategoryUpdateDtoValidator
{
    private readonly IBulletinCategoryRepository _categoryRepository;
    private readonly IBulletinCategorySpecificationBuilder _specificationBuilder;

    /// <inheritdoc/>
    public BulletinCategoryUpdateDtoValidator
        (
            IBulletinCategoryRepository categoryRepository,
            IBulletinCategorySpecificationBuilder specificationBuilder
        )
    {
        _categoryRepository = categoryRepository;
        _specificationBuilder = specificationBuilder;

        RuleFor(updateDto => updateDto.ParentCategoryId)
            .SetAsyncValidator(new ParentCategoryValidator<BulletinCategoryUpdateDtoForValidating>(_categoryRepository));

        RuleFor(updateDto => updateDto.CategoryName)
            .NotEmpty()
            .Length(3, 50)
            .Matches("^[а-яА-Яa-zA-Z\\s]+$").WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z) and spaces")
            .SetAsyncValidator(new CategoryNameValidator<BulletinCategoryUpdateDtoForValidating>(_categoryRepository, _specificationBuilder));
    }
}
