using BulletinBoard.AppServices.Contexts.Bulletin.Validators.ValidatorBase;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators
{
    public interface IBulletinCategoryUpdateDtoValidator : IValidator<BulletinCategoryUpdateDto>
    {
    }
}
