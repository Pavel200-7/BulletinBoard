using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.ForValidating;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Domain.Entities.Bulletin;
using FluentValidation.Results;



namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinCharacteristicService : IBulletinCharacteristicService
{
    private readonly IBulletinCharacteristicRepository _characteristicRepository;
    private readonly IBulletinCharacteristicDtoValidatorFacade _validator;
    private readonly IBulletinCharacteristicSpecificationBuilder _specificationBuilder;

    /// <inheritdoc/>
    public BulletinCharacteristicService
        (
            IBulletinCharacteristicRepository bulletinCharacteristicRepository,
            IBulletinCharacteristicDtoValidatorFacade bulletinCharacteristicDtoValidatorFacade,
            IBulletinCharacteristicSpecificationBuilder specificationBuilder
        )
    {
        _characteristicRepository = bulletinCharacteristicRepository;
        _validator = bulletinCharacteristicDtoValidatorFacade;
        _specificationBuilder = specificationBuilder;
    }


    /// <inheritdoc/>
    public async Task<BulletinCharacteristicDto> GetByIdAsync(Guid id)
    {
        BulletinCharacteristicDto? outputCharacteristicDto = await _characteristicRepository.GetByIdAsync(id);

        if (outputCharacteristicDto is null)
        {
            string errorMessage = $"The characteristic with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return outputCharacteristicDto;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<BulletinCharacteristicDto>> GetAsync(BulletinCharacteristicFilterDto сharacteristicDto)
    {
        if (сharacteristicDto.IsUsedName)
        {
            _specificationBuilder.WhereName(сharacteristicDto.Name);
        }
        else if (сharacteristicDto.IsUsedNameContains)
        {
            _specificationBuilder.WhereNameContains(сharacteristicDto.Name);
        }

        if (сharacteristicDto.IsUsedCategory)
        {
            _specificationBuilder.WhereCategoryId(сharacteristicDto.CategoryId);
        }

        ExtendedSpecification<BulletinCharacteristic> specification = _specificationBuilder
            .Build();

        IReadOnlyCollection<BulletinCharacteristicDto> characteristicDtoCollection = await _characteristicRepository.FindAsync(specification);

        return characteristicDtoCollection;
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicDto> CreateAsync(BulletinCharacteristicCreateDto сharacteristicDto)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(сharacteristicDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationExeption(validationResult.ToDictionary());
        }

        BulletinCharacteristicDto outputCharacteristicDto = await _characteristicRepository.CreateAsync(сharacteristicDto);

        return outputCharacteristicDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinCharacteristicDto> UpdateAsync(Guid id, BulletinCharacteristicUpdateDto сharacteristicDto)
    {
        var сharacteristicDtoForValidating = await GetDtoForValidatingUpdateDtoThrowNotFound(id, сharacteristicDto);
        ValidationResult validationResult = await _validator.ValidateAsync(сharacteristicDtoForValidating);
        if (!validationResult.IsValid)
        {
            throw new ValidationExeption(validationResult.ToDictionary());
        }

        BulletinCharacteristicDto? outputCharacteristicDto = await _characteristicRepository.UpdateAsync(id, сharacteristicDto);

        // Эти 4 строки не обязательны если есть GetDtoForValidatingUpdateDtoThrowNotFound.
        if (outputCharacteristicDto is null)
        {
            string errorMessage = $"The characteristic with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return outputCharacteristicDto!;
    }

    /// <inheritdoc/>
    private async Task<BulletinCharacteristicUpdateDtoForValidating> GetDtoForValidatingUpdateDtoThrowNotFound(Guid id, BulletinCharacteristicUpdateDto сharacteristicDto)
    {
        BulletinCharacteristicDto? characteristicBaseDto = await _characteristicRepository.GetByIdAsync(id);
        if (characteristicBaseDto is null)
        {
            string errorMessage = $"The characteristic with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

         return new BulletinCharacteristicUpdateDtoForValidating()
        {
            Name = сharacteristicDto.Name,
            CategoryId = characteristicBaseDto.CategoryId
        };
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        bool isOnDeleting = await _characteristicRepository.DeleteAsync(id);

        if (!isOnDeleting)
        {
            string errorMessage = $"The bulletin with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return isOnDeleting;

    }

}
