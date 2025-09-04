using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Errors.ErrorsList;
using BulletinBoard.Domain.Entities.Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services
{
    public sealed class BulletinCategoryService  : IBulletinCategoryService 
    {
        private IBulletinCategoryRepository repository;
        private IBulletinCategoryCreateDtoValidator validator;

        public BulletinCategoryService(IBulletinCategoryRepository bulletinCategoryRepository, IBulletinCategoryCreateDtoValidator BulletinCategoryCreateDtoValidator) 
        {
            repository = bulletinCategoryRepository;
            validator = BulletinCategoryCreateDtoValidator;
        }

        public Task<BulletinCategoryDto> CreateAsync(BulletinCategoryCreateDto category)
        {
            ErrorsDictionaryValidating errorsDictionary = validator.Validate(category);
            if (!errorsDictionary.IsEmpty())
            {
                // Нужно описать ошибку
            }

            // нужно описать логику работы с БД

            throw new NotImplementedException();

            //return BulletinCategoryDto
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

        public Task<BulletinCategoryDto> UpdateAsync(Guid id, BulletinCategoryUpdateDto category)
        {
            throw new NotImplementedException();
        }
    }
}
