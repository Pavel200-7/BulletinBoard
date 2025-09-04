using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator
{
    public sealed class BulletinCategoryUpdateDtoValidator : AbstractValidator<BulletinCategoryUpdateDto>, IBulletinCategoryUpdateDtoValidator
    {
        public BulletinCategoryUpdateDtoValidator()
        {
            RuleFor(category => category.CategoryName)
                .NotEmpty().WithMessage("The field is requered");
        }
    }
}
