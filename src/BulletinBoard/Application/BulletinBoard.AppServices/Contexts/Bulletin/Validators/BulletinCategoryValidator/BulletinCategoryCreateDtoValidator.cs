using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators.CategoryNameValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators.ParentCategoryIdValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator
{
    public sealed class BulletinCategoryCreateDtoValidator : AbstractValidator<BulletinCategoryCreateDto>, IBulletinCategoryCreateDtoValidator
    {
        private IBulletinCategoryRepository _categoryRepository;

        public BulletinCategoryCreateDtoValidator(IBulletinCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(bulletinCategoryCreateDto => bulletinCategoryCreateDto.ParentCategoryId)
                .SetAsyncValidator(new ExistingParrentCategoryValidator<BulletinCategoryCreateDto>(_categoryRepository))
                    .When(bulletinCategoryCreateDto => bulletinCategoryCreateDto.ParentCategoryId != null)
                .SetAsyncValidator(new ParrentCategoryIsNotLeafyValidator<BulletinCategoryCreateDto>(_categoryRepository))
                    .When(bulletinCategoryCreateDto => bulletinCategoryCreateDto.ParentCategoryId != null);

            RuleFor(bulletinCategoryCreateDto => bulletinCategoryCreateDto.CategoryName)
                .NotEmpty()
                .Length(3, 50)
                .Matches("^[а-яА-Яa-zA-Z\\s]+$").WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z) and spaces")
                .SetAsyncValidator(new UniqueCategoryNameValidator<BulletinCategoryCreateDto>(_categoryRepository));

            RuleFor(bulletinCategoryCreateDto => bulletinCategoryCreateDto.IsLeafy)
                .NotNull();
        }
    }
}
