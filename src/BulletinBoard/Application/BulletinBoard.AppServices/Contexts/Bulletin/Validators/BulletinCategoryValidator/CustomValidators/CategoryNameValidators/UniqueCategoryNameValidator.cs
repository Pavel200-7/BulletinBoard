using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Domain.Entities.Bulletin;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators.CategoryNameValidators
{
    public class UniqueCategoryNameValidator<T> : AsyncPropertyValidator<T, string>
    {
        public override string Name => "UniqueCategoryNameValidator";

        private readonly IBulletinCategoryRepository _categoryRepository;
        private readonly IBulletinCategorySpecificationBuilder _specificationBuilder;

        public UniqueCategoryNameValidator
            (
            IBulletinCategoryRepository categoryRepository, 
            IBulletinCategorySpecificationBuilder specificationBuilder
            )
        {
            _categoryRepository = categoryRepository;
            _specificationBuilder = specificationBuilder;
        }

        public override async Task<bool> IsValidAsync(ValidationContext<T> context, string categoryName, CancellationToken cancellation)
        {
            ExtendedSpecification<BulletinCategory> specification = _specificationBuilder
                .WhereCategoryName(categoryName)
                .Build();

            var categories = await _categoryRepository.FindAsync(specification);

            return !categories.Any();
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "A category with that name already exists.";
    }
}
