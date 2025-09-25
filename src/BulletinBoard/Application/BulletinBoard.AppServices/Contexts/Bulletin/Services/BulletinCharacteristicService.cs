using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.FilterDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.UpdateDto;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinCharacteristicService : BaseCRUDService
    <
    BulletinCharacteristicDto,
    BulletinCharacteristicCreateDto,
    BulletinCharacteristicUpdateDto,
    BulletinCharacteristicUpdateDtoForValidating,
    IBulletinCharacteristicRepository,
    IBulletinCharacteristicDtoValidatorFacade
    >, IBulletinCharacteristicService
{
    /// <inheritdoc/>
    protected override string EntityName { get; } = "characteristic";

    private readonly IBulletinCharacteristicSpecificationBuilder _specificationBuilder;

    /// <inheritdoc/>
    public BulletinCharacteristicService
        (
        IBulletinCharacteristicRepository repository,
        IBulletinCharacteristicDtoValidatorFacade validator,
        IMapper autoMapper,
        IBulletinCharacteristicSpecificationBuilder specificationBuilder
        ) : base(repository, validator, autoMapper)
    {
        _specificationBuilder = specificationBuilder;
    }

    /// <inheritdoc/>
    protected override async Task<BulletinCharacteristicUpdateDtoForValidating> GetUpdateValidationDto(Guid id, BulletinCharacteristicUpdateDto updateDto)
    {
        BulletinCharacteristicDto? characteristicBaseDto = await _repository.GetByIdAsync(id);
        
        if (characteristicBaseDto is null) { throw new NotFoundException(GetNotFoundIdMessage(id)); }

        var validatinoDto = _autoMapper.Map<BulletinCharacteristicUpdateDtoForValidating>(characteristicBaseDto);
        _autoMapper.Map(updateDto, validatinoDto);

        return validatinoDto;
    }


    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinCharacteristicDto>> GetAsync(BulletinCharacteristicFilterDto сharacteristicFilterDto)
    {
        if (сharacteristicFilterDto.IsUsedName)
        {
            _specificationBuilder.WhereName(сharacteristicFilterDto.Name);
        }
        else if (сharacteristicFilterDto.IsUsedNameContains)
        {
            _specificationBuilder.WhereNameContains(сharacteristicFilterDto.Name);
        }

        if (сharacteristicFilterDto.IsUsedCategory)
        {
            _specificationBuilder.WhereCategoryId(сharacteristicFilterDto.CategoryId);
        }

        ExtendedSpecification<BulletinCharacteristic> specification = _specificationBuilder
            .Build();

        IReadOnlyCollection<BulletinCharacteristicDto> characteristicDtoCollection = await _repository.FindAsync(specification);

        return characteristicDtoCollection;
    }

    /// <inheritdoc/>

    public async Task<IReadOnlyCollection<BulletinCharacteristicDto>> GetByCategoryFilter(Guid categoryId)
    {
        ExtendedSpecification<BulletinCharacteristic> specification = _specificationBuilder
            .WhereCategoryId(categoryId)
            .OrderByName(true)
            .Build();

        IReadOnlyCollection<BulletinCharacteristicDto> characteristicDtoCollection = await _repository.FindAsync(specification);

        return characteristicDtoCollection;
    }
}
