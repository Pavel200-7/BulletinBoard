using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinViewsCountValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.ForValidating;
using BulletinBoard.Contracts.Errors.Exeptions;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinViewsCountService : BaseCRUDService
    <
    BulletinViewsCountDto,
    BulletinViewsCountCreateDto,
    BulletinViewsCountUpdateDto,
    BulletinViewsCountUpdateDtoForValidating,
    IBulletinViewsCountRepository,
    IBulletinViewsCountDtoValidatorFacade
    >, IBulletinViewsCountService
{
    /// <inheritdoc/>
    protected override string EntityName { get; } = "views count";

    private readonly IBulletinViewsCountSpecificationBuilder _specificationBuilder;


    /// <inheritdoc/>
    public BulletinViewsCountService
        (
        IBulletinViewsCountRepository repository,
        IBulletinViewsCountDtoValidatorFacade validator,
        IMapper autoMapper,
        IBulletinViewsCountSpecificationBuilder specificationBuilder
        ) : base(repository, validator, autoMapper)
    {
        _specificationBuilder = specificationBuilder;
    }

    /// <inheritdoc/>
    protected override Task<BulletinViewsCountUpdateDtoForValidating> GetUpdateValidationDto(Guid id, BulletinViewsCountUpdateDto updateDto)
    {
        var validatinoDto = _autoMapper.Map<BulletinViewsCountUpdateDtoForValidating>(updateDto);
        return Task.FromResult(validatinoDto);
    }




    /// <inheritdoc/>
    public async Task<BulletinViewsCountDto> IncreaseViewsCountAsync(Guid bulletinId, CancellationToken cancellationToken)
    {
        BulletinViewsCountDto? viewsCountDto = await _repository.IncreaseViewsCountAsync(bulletinId, cancellationToken);
        if (viewsCountDto is null)
        {
            string errorMessage = $"The {EntityName} with bulletin id {bulletinId} is not found.";
            throw new NotFoundException(errorMessage);
        }

        return viewsCountDto;
    }
}
