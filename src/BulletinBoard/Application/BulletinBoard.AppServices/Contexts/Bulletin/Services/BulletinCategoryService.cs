using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.IValidators;
using BulletinBoard.Domain.Entities.Bulletin;

using BulletinBoard.Contracts.Errors.ErrorsList;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services
{
    public sealed class BulletinCategoryService  : IBulletinCategoryService 
    {
        private IBulletinCategoryRepository repository;
        private IBulletinCategoryValidator validator;

        public BulletinCategoryService(IBulletinCategoryRepository bulletinCategoryRepository, IBulletinCategoryValidator bulletinCategoryValidator) 
        {
            repository = bulletinCategoryRepository;
            validator = bulletinCategoryValidator;
        }

        public Task<BulletinCategoryDto> CreateAsync(BulletinCategoryCreateDto category)
        {
            BulletinCategory bulletinCategory = new BulletinCategory();

            ErrorsDictionaryValidating errorsDictionary = validator.Validate(bulletinCategory);
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
