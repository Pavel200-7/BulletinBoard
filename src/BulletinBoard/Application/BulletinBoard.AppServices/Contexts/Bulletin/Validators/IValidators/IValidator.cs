using BulletinBoard.Contracts.Errors.ErrorsList;
using BulletinBoard.Domain.Base;
using BulletinBoard.Domain.Entities.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.IValidators
{
    public interface IValidator<T> where T : EntityBase
    {
        public ErrorsDictionaryValidating Validate(T entity);
    }
}
