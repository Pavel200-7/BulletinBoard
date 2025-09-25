using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.ForValidating;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.ForValidating;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Domain.Entities.Bulletin;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinCharacteristicValueService : BaseCRUDService
    <
    BulletinCharacteristicValueDto,
    BulletinCharacteristicValueCreateDto,
    BulletinCharacteristicValueUpdateDto,
    BulletinCharacteristicValueUpdateDtoForValidating,
    IBulletinCharacteristicValueRepository,
    IBulletinCharacteristicValueDtoValidatorFacade
    >, IBulletinCharacteristicValueService
{
    /// <inheritdoc/>
    protected override string EntityName { get; } = "characteristic value";

    private readonly IBulletinCharacteristicValueSpecificationBuilder _specificationBuilder;

    /// <inheritdoc/>
    public BulletinCharacteristicValueService
        (
        IBulletinCharacteristicValueRepository repository,
        IBulletinCharacteristicValueDtoValidatorFacade validator,
        IMapper autoMapper,
        IBulletinCharacteristicValueSpecificationBuilder specificationBuilder
        ) : base(repository, validator, autoMapper)
    {
        _specificationBuilder = specificationBuilder;
    }

    /// <inheritdoc/>
    protected override async Task<BulletinCharacteristicValueUpdateDtoForValidating> GetUpdateValidationDto(Guid id, BulletinCharacteristicValueUpdateDto updateDto)
    {
        BulletinCharacteristicValueDto? characteristicValueBaseDto = await _repository.GetByIdAsync(id);
        
        if (characteristicValueBaseDto is null) { throw new NotFoundException(GetNotFoundIdMessage(id)); }
        
        var validatinoDto = _autoMapper.Map<BulletinCharacteristicValueUpdateDtoForValidating>(characteristicValueBaseDto);
        _autoMapper.Map(updateDto, validatinoDto);

        return validatinoDto;
    }


    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinCharacteristicValueDto>> GetAsync(BulletinCharacteristicValueFilterDto сharacteristicValueFilterDto)
    {
        if (сharacteristicValueFilterDto.IsUsedCharacteristicId)
        {
            _specificationBuilder.WhereCharacteristicId(сharacteristicValueFilterDto.CharacteristicId);
        }

        if (сharacteristicValueFilterDto.IsUsedValue)
        {
            _specificationBuilder.WhereValue(сharacteristicValueFilterDto.Value);
        }
        else if (сharacteristicValueFilterDto.IsUsedValueContains)
        {
            _specificationBuilder.WhereValueContains(сharacteristicValueFilterDto.Value);
        }

        ExtendedSpecification<BulletinCharacteristicValue> specification = _specificationBuilder
            .Build();

        IReadOnlyCollection<BulletinCharacteristicValueDto> characteristicValueCollection = await _repository.FindAsync(specification);

        return characteristicValueCollection;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinCharacteristicValueDto>> GetByCharacteristicAsync(Guid characteristicId)
    {
        ExtendedSpecification<BulletinCharacteristicValue> specification = _specificationBuilder
            .WhereCharacteristicId(characteristicId)
            .OrderByValue(true)
            .Build();

        IReadOnlyCollection<BulletinCharacteristicValueDto> characteristicValueDtoCollection = await _repository.FindAsync(specification);

        return characteristicValueDtoCollection;
    }
}
