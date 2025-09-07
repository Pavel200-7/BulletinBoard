using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators.ParentCategoryIdValidators
{
    public class ParrentCategoryIsNotLeafyValidator<T> : AsyncPropertyValidator<T, Guid>
    {
        public override string Name => "ParrentCategoryIsNotLeafyValidator";

        private readonly IBulletinCategoryRepository _categoryRepository;

        public ParrentCategoryIsNotLeafyValidator(IBulletinCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public override async Task<bool> IsValidAsync(ValidationContext<T> context, Guid categoryId, CancellationToken cancellation)
        {
            return !await _categoryRepository.IsTheCategoryLeafy(categoryId);
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "Parrent category with this id is Leafy and can not have children.";
    }
}
