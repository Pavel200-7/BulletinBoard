using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.ForValidating;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristic.ForValidating;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.ForValidating;
using BulletinBoard.Contracts.Errors.Exeptions;
using FluentValidation.Results;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinCharacteristicComparisonService : BaseCRUDService
    <
    BulletinCharacteristicComparisonDto,
    BulletinCharacteristicComparisonCreateDto,
    BulletinCharacteristicComparisonUpdateDto,
    BulletinCharacteristicComparisonUpdateDtoForValidating,
    IBulletinCharacteristicComparisonRepository,
    IBulletinCharacteristicComparisonDtoValidatorFacade
    >, IBulletinCharacteristicComparisonService
{
    /// <inheritdoc/>
    protected override string EntityName { get; } = "characteristic comparison";

    private readonly IBulletinCharacteristicComparisonSpecificationBuilder _specificationBuilder;

    /// <inheritdoc/>
    public BulletinCharacteristicComparisonService
        (
        IBulletinCharacteristicComparisonRepository repository,
        IBulletinCharacteristicComparisonDtoValidatorFacade validator,
        IMapper automapper,
        IBulletinCharacteristicComparisonSpecificationBuilder specificationBuilder
        ) : base(repository, validator, automapper)
    {
        _specificationBuilder = specificationBuilder;
    }

    /// <inheritdoc/>
    protected override async Task<BulletinCharacteristicComparisonUpdateDtoForValidating> GetUpdateValidationDto(Guid id, BulletinCharacteristicComparisonUpdateDto updateDto)
    {
        BulletinCharacteristicComparisonDto? characteristicComparisonBaseDto = await _repository.GetByIdAsync(id);
        if (characteristicComparisonBaseDto is null)
        {
            string errorMessage = $"The {EntityName} with id {id} is not found.";
            throw new NotFoundException(errorMessage);
        }
        var validatinoDto = _autoMapper.Map<BulletinCharacteristicComparisonUpdateDtoForValidating>(characteristicComparisonBaseDto);
        _autoMapper.Map(updateDto, validatinoDto);

        return validatinoDto;
    }
}
