using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Domain.Entities.Bulletin;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

public sealed class BulletinCategoryService : IBulletinCategoryService 
{
    private readonly IBulletinCategoryRepository _categoryRepository;
    private readonly IBulletinCategoryDtoValidatorFacade _validator;
    private readonly IBulletinCategorySpecificationBuilder _specificationBuilder;

    public BulletinCategoryService
        (
            IBulletinCategoryRepository bulletinCategoryRepository, 
            IBulletinCategoryDtoValidatorFacade bulletinCategoryDtoValidatorFacade,
            IBulletinCategorySpecificationBuilder specificationBuilder
        ) 
    {
        _categoryRepository = bulletinCategoryRepository;
        _validator = bulletinCategoryDtoValidatorFacade;
        _specificationBuilder = specificationBuilder;
    }


    public Task<BulletinCategoryDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<BulletinCategoryDto>> GetAsync(BulletinCategoryFilterDto categoryDto)
    {
        ExtendedSpecification<BulletinCategory> specification = _specificationBuilder
            .WhereParentId(categoryDto.ParentCategoryId)
            .WhereCategoryName(categoryDto.CategoryName)
            .Build();

        return await _categoryRepository.FindAsync(specification);
    }


    public async Task<BulletinCategoryDto> CreateAsync(BulletinCategoryCreateDto categoryDto)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(categoryDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationExeption(validationResult.ToDictionary());
        }

        BulletinCategoryDto ouputCategoryDto = await _categoryRepository.CreateAsync(categoryDto);
        await _categoryRepository.SaveChangesAsync();

        return ouputCategoryDto;
    }

    public Task<BulletinCategoryDto> UpdateAsync(Guid id, BulletinCategoryUpdateDto categoryDto)
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
