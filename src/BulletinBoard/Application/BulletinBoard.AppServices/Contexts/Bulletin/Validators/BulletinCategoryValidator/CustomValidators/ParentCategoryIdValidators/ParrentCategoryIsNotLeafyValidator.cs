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

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.CustomValidators.ParentCategoryIdValidators
{
    public class ParrentCategoryIsNotLeafyValidator<T> : AsyncPropertyValidator<T, Guid>
    {
        public override string Name => "ParrentCategoryIsNotLeafyValidator";

        private readonly IBulletinCategoryRepository _categoryRepository;
        private readonly IBulletinCategorySpecificationBuilder _specificationBuilder;


        public ParrentCategoryIsNotLeafyValidator(
            IBulletinCategoryRepository categoryRepository,
            IBulletinCategorySpecificationBuilder specificationBuilder
            )
        {
            _categoryRepository = categoryRepository;
            _specificationBuilder = specificationBuilder;
        }

        public override async Task<bool> IsValidAsync(ValidationContext<T> context, Guid categoryId, CancellationToken cancellation)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            return category!.IsLeafy;
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "Parrent category with this id is Leafy and can not have children.";
    }
}
