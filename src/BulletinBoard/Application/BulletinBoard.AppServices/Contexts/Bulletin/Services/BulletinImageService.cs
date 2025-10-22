using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders;
using BulletinBoard.AppServices.Contexts.Bulletin.Builders.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage.UpdateDto;
using BulletinBoard.Contracts.Errors.Exeptions;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinImageService : BaseCRUDService
    <
    BulletinImageDto,
    BulletinImageCreateDto,
    BulletinImageUpdateDto,
    BulletinImageUpdateDtoForValidating,
    IBulletinImageRepository,
    IBulletinImageDtoValidatorFacade
    >, IBulletinImageService
{
    /// <inheritdoc/>
    protected override string EntityName { get; } = "image";

    private IBulletinImageSpecificationBuilder _specificationBuilder;
    private IUnitOfWorkBulletin _unitOfWork;
    private ILogger<BulletinImageService> _logger;

    /// <inheritdoc/>
    public BulletinImageService
        (
        IBulletinImageRepository repository,
        IBulletinImageDtoValidatorFacade validator,
        IMapper autoMapper,
        IBulletinImageSpecificationBuilder specificationBuilder,
        IUnitOfWorkBulletin unitOfWork,
        ILogger<BulletinImageService> logger
        ) : base(repository, validator, autoMapper)
    {
        _specificationBuilder = specificationBuilder;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <inheritdoc/>
    protected override Task<BulletinImageUpdateDtoForValidating> GetUpdateValidationDto(Guid id, BulletinImageUpdateDto updateDto)
    {
        var validatinoDto = _autoMapper.Map<BulletinImageUpdateDtoForValidating>(updateDto);
        return Task.FromResult(validatinoDto);
    }

    /// <inheritdoc/>
    public new async Task<BulletinImageDto> CreateAsync(BulletinImageCreateDto createDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Для создания изображения была передана дто: {JsonSerializer.Serialize(createDto)}");
        await _validator.ValidateThrowValidationExeptionAsync(createDto);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            if (createDto.IsMain) 
            {
                Guid bulletinId = createDto.BulletinId;
                await UnSetMain(bulletinId, cancellationToken); 
            }

            BulletinImageDto outputDto = await _repository.CreateAsync(createDto, cancellationToken);

            await _unitOfWork.CommitTransactionAsync();

            return outputDto;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<BulletinImageDto?> SetMain(Guid id, CancellationToken cancellationToken)
    {
        BulletinImageDto? outputDto = await _repository.GetByIdAsync(id);
        if (outputDto is null) { throw new NotFoundException(GetNotFoundIdMessage(id)); }

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            Guid bulletinId = outputDto.BulletinId;
            await UnSetMain(bulletinId, cancellationToken);

            outputDto.IsMain = true;
            Guid updatingImageId = outputDto.Id;
            BulletinImageUpdateDto imageMainUpdateDto = _autoMapper.Map<BulletinImageUpdateDto>(outputDto);
            outputDto = await _repository.UpdateAsync(updatingImageId, imageMainUpdateDto, cancellationToken);

            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation($"Изображение с id {id} теперь считается основным.");

            return outputDto;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    /// <inheritdoc/>
    private async Task<BulletinImageDto?> UnSetMain(Guid bulletinId, CancellationToken cancellationToken)
    {
        BulletinImageDto? mainImage = await GetMainAsync(bulletinId);
        if (mainImage == null) { return null; }
 
        mainImage.IsMain = false;
        Guid mainImageId = mainImage.Id;
        BulletinImageUpdateDto mainImageUpdateDto = _autoMapper.Map<BulletinImageUpdateDto>(mainImage);

        BulletinImageDto? imageDto = await _repository.UpdateAsync(mainImageId, mainImageUpdateDto, cancellationToken);

        _logger.LogInformation($"Изображение с id {mainImageId} перестало быть титульным.");
        
        return imageDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinImageDto?> GetMainAsync(Guid bulletinId)
    {
        var specification = _specificationBuilder
            .WhereBelletinId(bulletinId)
            .WhereIsMain(true)
            .Build();

        IReadOnlyCollection<BulletinImageDto> mainImageCollection = await _repository.FindAsync(specification);
        if (mainImageCollection.Count == 0) { return null; }
        BulletinImageDto mainImage = mainImageCollection.First();

        _logger.LogInformation($"Было найдено титульное изображение {JsonSerializer.Serialize(mainImage)}");

        return mainImage;
    }
}
