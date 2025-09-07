using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
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

        public UniqueCategoryNameValidator(IBulletinCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public override async Task<bool> IsValidAsync(ValidationContext<T> context, string categoryName, CancellationToken cancellation)
        {
            return !await _categoryRepository.IsTheCategoryNameExist(categoryName);
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "A category with that name already exists.";
    }
}
