using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Mapping.IMappingServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.ForValidating;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Domain.Entities.Bulletin;



namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public sealed class BulletinCategoryService : BaseCRUDService
    <
    BulletinCategoryDto,
    BulletinCategoryCreateDto,
    BulletinCategoryUpdateDto,
    BulletinCategoryUpdateDtoForValidating,
    IBulletinCategoryRepository,
    IBulletinCategoryDtoValidatorFacade
    >, IBulletinCategoryService
{
    /// <inheritdoc/>
    protected override string EntityName { get; } = "category";

    private readonly IBulletinCategorySpecificationBuilder _specificationBuilder;
    private readonly IBulletinCategoryMappingService _mapper;

    /// <inheritdoc/>
    public BulletinCategoryService
        (
        IBulletinCategoryRepository repository,
        IBulletinCategoryDtoValidatorFacade validator,
        IMapper automapper,
        IBulletinCategorySpecificationBuilder specificationBuilder,
        IBulletinCategoryMappingService mapper
        ) : base(repository, validator, automapper)
    {
        _specificationBuilder = specificationBuilder;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    protected override Task<BulletinCategoryUpdateDtoForValidating> GetUpdateValidationDto(Guid id, BulletinCategoryUpdateDto updateDto)
    {
        var validatinoDto = _autoMapper.Map<BulletinCategoryUpdateDtoForValidating>(updateDto);
        return Task.FromResult(validatinoDto);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinCategoryDto>> GetAsync(BulletinCategoryFilterDto categoryFilterDto)
    {
        if (categoryFilterDto.IsUsedParentCategoryId)
        {
            _specificationBuilder.WhereParentId(categoryFilterDto.ParentCategoryId);
        }

        if (categoryFilterDto.IsUsedCategoryName)
        {
            _specificationBuilder.WhereCategoryName(categoryFilterDto.CategoryName);
        }

        ExtendedSpecification<BulletinCategory> specification = _specificationBuilder
            .Build();

        IReadOnlyCollection<BulletinCategoryDto> categoryDtoCollection = await _repository.FindAsync(specification);

        return categoryDtoCollection;
    }


    /// <inheritdoc/>
    public async Task<BulletinCategoryReadAllDto> GetAllAsync()
    {
        ExtendedSpecification<BulletinCategory> specificationWithouFilter = _specificationBuilder
            .Build();

        IReadOnlyCollection<BulletinCategoryDto> allCategories = await _repository.FindAsync(specificationWithouFilter);
        BulletinCategoryReadAllDto categoriesReadAllDto = await _mapper.ConvertToBulletinCategoryReadAllDto(allCategories);

        return categoriesReadAllDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinCategoryReadSingleDto> GetSingleAsync(Guid id)
    {
        List<BulletinCategoryDto> CategoriesList = new List<BulletinCategoryDto>();
        Guid? searchingCategoryId = id;

        while (searchingCategoryId != null)
        {
            var currentCategory = await _repository.GetByIdAsync(searchingCategoryId.Value);
            if (currentCategory is null)
            {
                string errorMessage = $"The category with id {searchingCategoryId} is not found.";
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
