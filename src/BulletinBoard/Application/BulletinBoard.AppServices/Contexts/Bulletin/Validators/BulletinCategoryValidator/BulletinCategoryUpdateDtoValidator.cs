using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators;
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
                .SetAsyncValidator(new ParentCategoryValidator<BulletinCategoryUpdateDto>(_categoryRepository));

            RuleFor(BulletinCategoryUpdateDto => BulletinCategoryUpdateDto.CategoryName)
                .NotEmpty()
                .Length(3, 50)
                .Matches("^[а-яА-Яa-zA-Z\\s]+$").WithMessage("{PropertyName} can contain only letters (а-яА-Яa-zA-Z) and spaces")
                .SetAsyncValidator(new CategoryNameValidator<BulletinCategoryUpdateDto>(_categoryRepository, _specificationBuilder));
        }
    }
}
