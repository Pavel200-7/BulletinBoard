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

        public BulletinCategoryUpdateDtoValidator(IBulletinCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(bulletinCategoryCreateDto => bulletinCategoryCreateDto.ParentCategoryId)
                .SetAsyncValidator(new ExistingParrentCategoryValidator<BulletinCategoryUpdateDto>(_categoryRepository))
                    .When(bulletinCategoryCreateDto => bulletinCategoryCreateDto.ParentCategoryId != null)
                .SetAsyncValidator(new ParrentCategoryIsNotLeafyValidator<BulletinCategoryUpdateDto>(_categoryRepository))
                    .When(bulletinCategoryCreateDto => bulletinCategoryCreateDto.ParentCategoryId != null);

            RuleFor(bulletinCategoryCreateDto => bulletinCategoryCreateDto.CategoryName)
                .NotEmpty()
                .Length(3, 50)
                .Matches("^[а-яА-Яa-zA-Z\\s]+$").WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z) and spaces")
                .SetAsyncValidator(new UniqueCategoryNameValidator<BulletinCategoryUpdateDto>(_categoryRepository));
        }
    }
}
