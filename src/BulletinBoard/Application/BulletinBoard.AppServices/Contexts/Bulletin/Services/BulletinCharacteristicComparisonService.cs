using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.UpdateDto;
using BulletinBoard.Contracts.Errors.Exeptions;

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
        
        if (characteristicComparisonBaseDto is null) { throw new NotFoundException(GetNotFoundIdMessage(id)); }

        var validatinoDto = _autoMapper.Map<BulletinCharacteristicComparisonUpdateDtoForValidating>(characteristicComparisonBaseDto);
        _autoMapper.Map(updateDto, validatinoDto);

        return validatinoDto;
    }
}
