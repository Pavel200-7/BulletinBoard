using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators;
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
                .SetAsyncValidator(new ParentCategoryValidator<BulletinCategoryCreateDto>(_categoryRepository));

            RuleFor(bulletinCategoryCreateDto => bulletinCategoryCreateDto.CategoryName)
                .NotEmpty()
                .Length(3, 50)
                .Matches("^[а-яА-Яa-zA-Z\\s]+$").WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z) and spaces")
                .SetAsyncValidator(new CategoryNameValidator<BulletinCategoryCreateDto>(_categoryRepository, _specificationBuilder));

            RuleFor(bulletinCategoryCreateDto => bulletinCategoryCreateDto.IsLeafy)
                .NotNull();
        }
    }
}
