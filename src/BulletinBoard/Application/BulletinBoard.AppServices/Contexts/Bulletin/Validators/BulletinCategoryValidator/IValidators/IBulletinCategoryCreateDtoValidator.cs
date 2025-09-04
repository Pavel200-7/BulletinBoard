using BulletinBoard.AppServices.Contexts.Bulletin.Validators.ValidatorBase;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Errors.ErrorsList;
using BulletinBoard.Domain.Entities.Bulletin;
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
