using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Domain.Entities.Bulletin;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator
{
    public sealed class BulletinCategoryCreateDtoValidator : AbstractValidator<BulletinCategoryCreateDto>, IBulletinCategoryCreateDtoValidator
    {
        public BulletinCategoryCreateDtoValidator()
        {
            RuleFor(bulletinCategoryCreateDto => bulletinCategoryCreateDto.CategoryName)
                .NotEmpty().WithMessage("The field is requered");
        }
    }
}
