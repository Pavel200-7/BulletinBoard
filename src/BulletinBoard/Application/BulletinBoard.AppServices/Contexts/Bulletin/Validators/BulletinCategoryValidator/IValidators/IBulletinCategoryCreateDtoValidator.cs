using BulletinBoard.AppServices.Contexts.IValidator;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Domain.Entities.Bulletin;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.BulletinCategoryValidator.IValidators
{
    public interface IBulletinCategoryCreateDtoValidator : IValidator<BulletinCategoryCreateDto>
    {
    }
}
