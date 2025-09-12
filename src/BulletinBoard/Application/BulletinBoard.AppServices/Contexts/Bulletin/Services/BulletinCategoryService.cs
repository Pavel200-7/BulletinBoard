using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.MappingServices.IMappingServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Domain.Entities.Bulletin;
using FluentValidation.Results;
using System.Reflection.Metadata.Ecma335;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

public sealed class BulletinCategoryService : IBulletinCategoryService 
{
    private readonly IBulletinCategoryRepository _categoryRepository;
    private readonly IBulletinCategoryDtoValidatorFacade _validator;
    private readonly IBulletinCategorySpecificationBuilder _specificationBuilder;
    private readonly IBulletinCategoryMappingService _mapper;

    public BulletinCategoryService
        (
            IBulletinCategoryRepository bulletinCategoryRepository, 
            IBulletinCategoryDtoValidatorFacade bulletinCategoryDtoValidatorFacade,
            IBulletinCategorySpecificationBuilder specificationBuilder,
            IBulletinCategoryMappingService mapper
        ) 
    {
        _categoryRepository = bulletinCategoryRepository;
        _validator = bulletinCategoryDtoValidatorFacade;
        _specificationBuilder = specificationBuilder;
        _mapper = mapper;
    }


    public async Task<BulletinCategoryDto> GetByIdAsync(Guid id)
    {
        BulletinCategoryDto? outputCategoryDto = await _categoryRepository.GetByIdAsync(id);

        if (outputCategoryDto is null)
        {
            string errorMessage = $"The note with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return outputCategoryDto!;
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

        BulletinCategoryDto outputCategoryDto = await _categoryRepository.CreateAsync(categoryDto);
        await _categoryRepository.SaveChangesAsync();

        return outputCategoryDto;
    }

    public async Task<BulletinCategoryDto> UpdateAsync(Guid id, BulletinCategoryUpdateDto categoryDto)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(categoryDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationExeption(validationResult.ToDictionary());
        }

        BulletinCategoryDto? outputCategoryDto = await _categoryRepository.UpdateAsync(id, categoryDto);

        if (outputCategoryDto is null)
        {
            string errorMessage = $"The note with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        await _categoryRepository.SaveChangesAsync();

        return outputCategoryDto;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        bool isOnDeleting = await _categoryRepository.DeleteAsync(id);

        if (!isOnDeleting)
        {
            string errorMessage = $"The note with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        await _categoryRepository.SaveChangesAsync();

        return isOnDeleting;
    }

    public async Task<BulletinCategoryReadAllDto> GetAllAsync()
    {
        ExtendedSpecification<BulletinCategory> specificationWithouFilter = _specificationBuilder
            .Build();

        IReadOnlyCollection<BulletinCategoryDto> allCategories = await _categoryRepository.FindAsync(specificationWithouFilter);
        BulletinCategoryReadAllDto categoriesReadAllDto = await _mapper.ConvertToBulletinCategoryReadAllDto(allCategories);

        return categoriesReadAllDto;
    }

    public async Task<BulletinCategoryReadSingleDto> GetSingleAsync(Guid id)
    {
        List<BulletinCategoryDto> CategoriesList = new List<BulletinCategoryDto>();
        Guid? searchingCategoryId = id;

        while(searchingCategoryId != null)
        {
            var currentCategory = await _categoryRepository.GetByIdAsync(searchingCategoryId.Value);
            if (currentCategory is null) 
            {
                string errorMessage = $"The note with id {searchingCategoryId} is not found.";
                throw new NotFoundException(errorMessage);
            }

            CategoriesList.Add(currentCategory);
            searchingCategoryId = currentCategory.ParentCategoryId;
        }

        IReadOnlyCollection<BulletinCategoryDto> allCategories = CategoriesList.AsReadOnly();  
        BulletinCategoryReadSingleDto categoriesReadSingleDto = await _mapper.ConvertToBulletinCategoryReadSingleDto(allCategories);

        return categoriesReadSingleDto;
    }

    
}
