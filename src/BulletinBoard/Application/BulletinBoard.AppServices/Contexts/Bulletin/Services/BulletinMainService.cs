using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BelletinMain.ForValidating;



namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinMainService : BaseCRUDService
    <
    BulletinMainDto,
    BulletinMainCreateDto,
    BulletinMainUpdateDto,
    BulletinMainUpdateDtoForValidating,
    IBulletinMainRepository,
    IBulletinMainDtoValidatorFacade
    >, IBulletinMainService
{
    /// <inheritdoc/>
    protected override string EntityName { get; } = "bulletin";

    private readonly IBulletinMainSpecificationBuilder _specificationBuilder;

    /// <inheritdoc/>
    public BulletinMainService
        (
        IBulletinMainRepository repository,
        IBulletinMainDtoValidatorFacade validator,
        IMapper autoMapper,
        IBulletinMainSpecificationBuilder specificationBuilder
        ) : base ( repository, validator, autoMapper)
    {
        _specificationBuilder = specificationBuilder;
    }

    /// <inheritdoc/>
    protected override Task<BulletinMainUpdateDtoForValidating> GetUpdateValidationDto(Guid id, BulletinMainUpdateDto updateDto)
    {
        var validatinoDto = _autoMapper.Map<BulletinMainUpdateDtoForValidating>(updateDto);
        return Task.FromResult(validatinoDto);
    }
}
