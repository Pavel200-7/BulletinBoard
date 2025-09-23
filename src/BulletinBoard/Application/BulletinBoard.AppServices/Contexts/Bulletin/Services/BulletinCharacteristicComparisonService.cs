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
        await _validator.ValidateThrowValidationExeptionAsync(сharacteristicComparisonDto);
        BulletinCharacteristicComparisonDto outputCharacteristicComparisonDto = await _repository.CreateAsync(сharacteristicComparisonDto);
        return outputCharacteristicComparisonDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicComparisonDto> UpdateAsync(Guid id, BulletinCharacteristicComparisonUpdateDto сharacteristicComparisonDto)
    {
        var dtoForValidating = await GetDtoForValidatingUpdateDtoThrowNotFound(id, сharacteristicComparisonDto);
        await _validator.ValidateThrowValidationExeptionAsync(dtoForValidating);

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
        await _validator.ValidateBeforeDeletingThrowValidationExeptionAsync(id);
        bool isOnDeleting = await _repository.DeleteAsync(id);
        if (!isOnDeleting)
        {
            string errorMessage = $"The characteristic comparison with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return isOnDeleting;
    }
}
