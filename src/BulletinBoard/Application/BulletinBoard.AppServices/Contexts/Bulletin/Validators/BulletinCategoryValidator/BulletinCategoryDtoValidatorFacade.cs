using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.ValidatorBase;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator
{
    public sealed class BulletinCategoryDtoValidatorFacade : IBulletinCategoryDtoValidatorFacade
    {
        private readonly IBulletinCategoryCreateDtoValidator _bulletinCategoryCreateDtoValidator;
        private readonly IBulletinCategoryUpdateDtoValidator _bulletinCategoryUpdateDtoValidator;

        public BulletinCategoryDtoValidatorFacade
            (
                IBulletinCategoryCreateDtoValidator bulletinCategoryCreateDtoValidator, 
                IBulletinCategoryUpdateDtoValidator bulletinCategoryUpdateDtoValidator
            )
        {
            _bulletinCategoryCreateDtoValidator = bulletinCategoryCreateDtoValidator;
            _bulletinCategoryUpdateDtoValidator = bulletinCategoryUpdateDtoValidator;
        }

        public ValidationResult Validate(BulletinCategoryCreateDto entityDto)
        {
            return _bulletinCategoryCreateDtoValidator.Validate(entityDto);
        }

        public ValidationResult Validate(BulletinCategoryUpdateDto entityDto)
        {
            return _bulletinCategoryUpdateDtoValidator.Validate(entityDto);
        }
    }
}
