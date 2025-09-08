using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators.CategoryNameValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators.ParentCategoryIdValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator
{
    public sealed class BulletinCategoryUpdateDtoValidator : AbstractValidator<BulletinCategoryUpdateDto>, IBulletinCategoryUpdateDtoValidator
    {

        private readonly IBulletinCategoryRepository _categoryRepository;
        private readonly IBulletinCategorySpecificationBuilder _specificationBuilder;

        public BulletinCategoryUpdateDtoValidator
            (
                IBulletinCategoryRepository categoryRepository,
                IBulletinCategorySpecificationBuilder specificationBuilder
            )
        {
            _categoryRepository = categoryRepository;
            _specificationBuilder = specificationBuilder;

            RuleFor(BulletinCategoryUpdateDto => BulletinCategoryUpdateDto.ParentCategoryId)
                .NotNull()
                .When(x => x.ParentCategoryId != null)
                .DependentRules(() =>
                {
                    RuleFor(x => x.ParentCategoryId)
                        .SetAsyncValidator(new ExistingParrentCategoryValidator<BulletinCategoryUpdateDto>(_categoryRepository))
                        .DependentRules(() =>
                        {
                            RuleFor(x => x.ParentCategoryId)
                                .SetAsyncValidator(new ParrentCategoryIsNotLeafyValidator<BulletinCategoryUpdateDto>(_categoryRepository, _specificationBuilder));
                        });
                });

            RuleFor(BulletinCategoryUpdateDto => BulletinCategoryUpdateDto.CategoryName)
                .NotEmpty()
                .Length(3, 50)
                .Matches("^[а-яА-Яa-zA-Z\\s]+$").WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z) and spaces")
                .SetAsyncValidator(new UniqueCategoryNameValidator<BulletinCategoryUpdateDto>(_categoryRepository, _specificationBuilder));
        }
    }
}
