using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicValue.ForValidating;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Domain.Entities.Bulletin;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinCharacteristicValueService : IBulletinCharacteristicValueService
{
    private readonly IBulletinCharacteristicValueRepository _repository;
    private readonly IBulletinCharacteristicValueDtoValidatorFacade _validator;
    private readonly IBulletinCharacteristicValueSpecificationBuilder _specificationBuilder;

    /// <inheritdoc/>
    public BulletinCharacteristicValueService
        (
        IBulletinCharacteristicValueRepository repository,
        IBulletinCharacteristicValueDtoValidatorFacade validator,
        IBulletinCharacteristicValueSpecificationBuilder specificationBuilder
        )
    {
        _repository = repository;
        _validator = validator;
        _specificationBuilder = specificationBuilder;
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicValueDto> GetByIdAsync(Guid id)
    {
        BulletinCharacteristicValueDto? characteristicValueDto = await _repository.GetByIdAsync(id);
        if (characteristicValueDto is null)
        {
            string errorMessage = $"The characteristic value with id {id} is not found.";
            throw new NotFoundException(errorMessage); 
        }

        return characteristicValueDto;
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
    public async Task<BulletinCharacteristicValueDto> CreateAsync(BulletinCharacteristicValueCreateDto сharacteristicValueDto)
    {
        await _validator.ValidateThrowValidationExeptionAsync(сharacteristicValueDto);
        BulletinCharacteristicValueDto outputCharacteristicValueDto = await _repository.CreateAsync(сharacteristicValueDto);
        return outputCharacteristicValueDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicValueDto> UpdateAsync(Guid id, BulletinCharacteristicValueUpdateDto сharacteristicValueDto)
    {
        var сharacteristicValueDtoForValidating = await GetDtoForValidatingUpdateDtoThrowNotFound(id, сharacteristicValueDto);
        await _validator.ValidateThrowValidationExeptionAsync(сharacteristicValueDtoForValidating);

        BulletinCharacteristicValueDto? outputCharacteristicValueDto = await _repository.UpdateAsync(id, сharacteristicValueDto);
        // Эти 4 строки не обязательны если есть GetDtoForValidatingUpdateDtoThrowNotFound.
        if (outputCharacteristicValueDto is null)
        {
            string errorMessage = $"The characteristic value with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return outputCharacteristicValueDto!;

    }

    private async Task<BulletinCharacteristicValueUpdateDtoForValidating> GetDtoForValidatingUpdateDtoThrowNotFound(Guid id, BulletinCharacteristicValueUpdateDto сharacteristicValueDto)
    {
        BulletinCharacteristicValueDto? characteristicValueBaseDto = await _repository.GetByIdAsync(id);
        if (characteristicValueBaseDto is null)
        {
            string errorMessage = $"The characteristic value with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return new BulletinCharacteristicValueUpdateDtoForValidating()
        {
            CharacteristicId = characteristicValueBaseDto.CharacteristicId,
            Value = сharacteristicValueDto.Value,
        };
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        await _validator.ValidateBeforeDeletingThrowValidationExeptionAsync(id);
        bool isOnDeleting = await _repository.DeleteAsync(id);
        if (!isOnDeleting)
        {
            string errorMessage = $"The characteristic value with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return isOnDeleting;
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
