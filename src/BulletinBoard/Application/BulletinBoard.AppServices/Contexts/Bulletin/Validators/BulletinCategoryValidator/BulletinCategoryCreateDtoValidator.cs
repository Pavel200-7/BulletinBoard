using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators.CategoryNameValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators.ParentCategoryIdValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator
{
    public sealed class BulletinCategoryCreateDtoValidator : AbstractValidator<BulletinCategoryCreateDto>, IBulletinCategoryCreateDtoValidator
    {
        private readonly IBulletinCategoryRepository _categoryRepository;
        private readonly IBulletinCategorySpecificationBuilder _specificationBuilder;


        public BulletinCategoryCreateDtoValidator
            (
                IBulletinCategoryRepository categoryRepository,
                IBulletinCategorySpecificationBuilder specificationBuilder
            )
        {
            _categoryRepository = categoryRepository;
            _specificationBuilder = specificationBuilder;

            RuleFor(bulletinCategoryCreateDto => bulletinCategoryCreateDto.ParentCategoryId)
                .NotNull()
                .When(x => x.ParentCategoryId != null)
                .DependentRules(() =>
                {
                    RuleFor(x => x.ParentCategoryId)
                        .SetAsyncValidator(new ExistingParrentCategoryValidator<BulletinCategoryCreateDto>(_categoryRepository))
                        .DependentRules(() =>
                        {
                            RuleFor(x => x.ParentCategoryId)
                                .SetAsyncValidator(new ParrentCategoryIsNotLeafyValidator<BulletinCategoryCreateDto>(_categoryRepository, _specificationBuilder));
                        });
                });

            RuleFor(bulletinCategoryCreateDto => bulletinCategoryCreateDto.CategoryName)
                .NotEmpty()
                .Length(3, 50)
                .Matches("^[а-яА-Яa-zA-Z\\s]+$").WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z) and spaces")
                .SetAsyncValidator(new UniqueCategoryNameValidator<BulletinCategoryCreateDto>(_categoryRepository, _specificationBuilder));

            RuleFor(bulletinCategoryCreateDto => bulletinCategoryCreateDto.IsLeafy)
                .NotNull();
        }
    }
}
