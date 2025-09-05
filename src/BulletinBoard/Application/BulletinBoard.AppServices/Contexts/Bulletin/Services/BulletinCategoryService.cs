using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Errors;
using BulletinBoard.Contracts.Errors.Exeptions;
//using BulletinBoard.Contracts.Errors.ErrorsList;
using BulletinBoard.Domain.Entities.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services
{
    public sealed class BulletinCategoryService : IBulletinCategoryService 
    {
        private readonly IBulletinCategoryRepository _categoryRepository;
        private readonly IBulletinCategoryDtoValidatorFacade _validator;

        public BulletinCategoryService
            (
                IBulletinCategoryRepository bulletinCategoryRepository, 
                IBulletinCategoryDtoValidatorFacade bulletinCategoryDtoValidatorFacade
            ) 
        {
            _categoryRepository = bulletinCategoryRepository;
            _validator = bulletinCategoryDtoValidatorFacade;
        }


        public Task<BulletinCategoryDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BulletinCategoryDto> CreateAsync(BulletinCategoryCreateDto category)
        {
            var validationResult = _validator.Validate(category);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult.ToDictionary());
            }

            var CategoryDto = _categoryRepository.CreateAsync(category);
            _categoryRepository.SaveChangesAsync();

            return CategoryDto;
        }

        public Task<BulletinCategoryDto> UpdateAsync(Guid id, BulletinCategoryUpdateDto category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BulletinCategoryReadAllDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BulletinCategoryReadSingleDto> GetSingleAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        
    }
}
