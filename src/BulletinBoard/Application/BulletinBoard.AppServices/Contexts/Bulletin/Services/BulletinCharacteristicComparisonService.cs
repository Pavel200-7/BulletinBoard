using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.ForValidating;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.ForValidating;
using BulletinBoard.Contracts.Errors.Exeptions;
using FluentValidation.Results;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinCharacteristicComparisonService : IBulletinCharacteristicComparisonService
{
    private readonly IBulletinCharacteristicComparisonRepository _repository;
    private readonly IBulletinCharacteristicComparisonDtoValidatorFacade _validator;
    private readonly IBulletinCharacteristicComparisonSpecificationBuilder _specificationBuilder;

    /// <inheritdoc/>
    public BulletinCharacteristicComparisonService
        (
        IBulletinCharacteristicComparisonRepository repository,
        IBulletinCharacteristicComparisonDtoValidatorFacade validator,
        IBulletinCharacteristicComparisonSpecificationBuilder specificationBuilder
        )
    {
        _repository = repository;
        _validator = validator;
        _specificationBuilder = specificationBuilder;
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicComparisonDto> GetByIdAsync(Guid id)
    {
        BulletinCharacteristicComparisonDto? outputcharacteristicComparisonDto = await _repository.GetByIdAsync(id);
        if (outputcharacteristicComparisonDto is null)
        {
            string errorMessage = $"The characteristic comparison with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return outputcharacteristicComparisonDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicComparisonDto> CreateAsync(BulletinCharacteristicComparisonCreateDto сharacteristicComparisonDto)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(сharacteristicComparisonDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationExeption(validationResult.ToDictionary());
        }

        BulletinCharacteristicComparisonDto outputcharacteristicComparisonDto = await _repository.CreateAsync(сharacteristicComparisonDto);

        return outputcharacteristicComparisonDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicComparisonDto> UpdateAsync(Guid id, BulletinCharacteristicComparisonUpdateDto сharacteristicComparisonDto)
    {
        var сharacteristicComparisonDtoForValidating = await GetDtoForValidatingUpdateDtoThrowNotFound(id, сharacteristicComparisonDto);
        ValidationResult validationResult = await _validator.ValidateAsync(сharacteristicComparisonDtoForValidating);
        if (!validationResult.IsValid)
        {
            throw new ValidationExeption(validationResult.ToDictionary());
        }

        BulletinCharacteristicComparisonDto? outputcharacteristicComparisonDto = await _repository.UpdateAsync(id, сharacteristicComparisonDto);
        if (outputcharacteristicComparisonDto is null)
        {
            string errorMessage = $"The characteristic comparison with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return outputcharacteristicComparisonDto;
    }
    private async Task<BulletinCharacteristicComparisonUpdateDtoForValidating> GetDtoForValidatingUpdateDtoThrowNotFound(Guid id, BulletinCharacteristicComparisonUpdateDto сharacteristicComparisonDto)
    {
        BulletinCharacteristicComparisonDto? characteristicComparisonBaseDto = await _repository.GetByIdAsync(id);
        if (characteristicComparisonBaseDto is null)
        {
            string errorMessage = $"The characteristic comparison with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return new BulletinCharacteristicComparisonUpdateDtoForValidating()
        {
            BulletinId = characteristicComparisonBaseDto.BulletinId,
            CharacteristicId = characteristicComparisonBaseDto.CharacteristicId,
            CharacteristicValueId = сharacteristicComparisonDto.CharacteristicValueId
        };
    }



    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        ValidationResult validationResult = await _validator.ValidateBeforeDeletingAsync(id);
        if (!validationResult.IsValid)
        {
            throw new ValidationExeption(validationResult.ToDictionary());
        }

        bool isOnDeleting = await _repository.DeleteAsync(id);
        if (!isOnDeleting)
        {
            string errorMessage = $"The characteristic comparison with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return isOnDeleting;
    }
}
