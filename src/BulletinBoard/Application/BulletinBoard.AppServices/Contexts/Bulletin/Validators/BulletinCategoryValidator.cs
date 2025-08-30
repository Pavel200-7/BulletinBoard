using BulletinBoard.AppServices.Contexts.Bulletin.Validators.IValidators;
using BulletinBoard.Contracts.Errors.ErrorsList;
using BulletinBoard.Domain.Entities.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators
{
    public class BulletinCategoryValidator : IBulletinCategoryValidator
    {
        public ErrorsDictionaryValidating Validate(BulletinCategory bulletinCategory)
        {
            ErrorsDictionaryValidating errors = new ErrorsDictionaryValidating();

            return errors;
        }
    }
}
