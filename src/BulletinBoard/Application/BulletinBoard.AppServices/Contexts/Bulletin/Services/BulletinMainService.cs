using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BelletinMain.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinMain.UpdateDto;
using BulletinBoard.Contracts.Errors.Exeptions;



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

    /// <inheritdoc/>
    public async Task<BulletinMainDto> BlockBulletin(Guid id, CancellationToken cancellationToken)
    {
        BulletinMainDto bulletinDto = await _repository.GetByIdAsync(id);
        if (bulletinDto is null) { throw new NotFoundException(GetNotFoundIdMessage(id)); }
        bool blocked = true;
        bulletinDto = await _repository.SetBulletinBlockStatusAsync(id, blocked, cancellationToken);


        return bulletinDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinMainDto> UnBlockBulletin(Guid id, CancellationToken cancellationToken)
    {
        BulletinMainDto bulletinDto = await _repository.GetByIdAsync(id);
        if (bulletinDto is null) { throw new NotFoundException(GetNotFoundIdMessage(id)); }
        bool blocked = false;
        bulletinDto = await _repository.SetBulletinBlockStatusAsync(id, blocked, cancellationToken);

        return bulletinDto;
    }
}
