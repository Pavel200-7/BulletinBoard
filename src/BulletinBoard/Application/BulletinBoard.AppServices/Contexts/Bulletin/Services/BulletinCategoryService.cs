using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Errors.Exeptions;
using FluentValidation.Results;



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

        public async Task<BulletinCategoryDto> CreateAsync(BulletinCategoryCreateDto category)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(category);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult.ToDictionary());
            }

            BulletinCategoryDto CategoryDto = await _categoryRepository.CreateAsync(category);
            await _categoryRepository.SaveChangesAsync();

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
