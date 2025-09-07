using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators
{
    public interface IBulletinCategoryDtoValidatorFacade
    {
        public Task<ValidationResult> ValidateAsync(BulletinCategoryCreateDto entityDto);

        public Task<ValidationResult> ValidateAsync(BulletinCategoryUpdateDto entityDto);
    }
}
