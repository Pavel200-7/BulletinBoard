using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using FluentValidation.Results;
using System.Threading;

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

        public async Task<ValidationResult> ValidateAsync(BulletinCategoryCreateDto entityDto)
        {
            return await _bulletinCategoryCreateDtoValidator.ValidateAsync(entityDto);
        }

        public async Task<ValidationResult> ValidateAsync(BulletinCategoryUpdateDto entityDto)
        {
            return await _bulletinCategoryUpdateDtoValidator.ValidateAsync(entityDto);
        }
    }
}
