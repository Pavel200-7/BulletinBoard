using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.Agrigates.Belletin;
using BulletinBoard.Contracts.Bulletin.Agrigates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BelletinMain.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinImage.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinViewsCount.CreateDto;



namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinService : IBulletinService
{
    //private IBulletinMainService _mainService { get; set; }
    //private IBulletinCharacteristicComparisonService _characteristicComparisonService { get; set; }
    //private IBulletinViewsCountService _viewsCountService { get; set; }
    //private IBulletinImageService _imageService { get; set; }
    private IBulletinReposotory _bulletinReposotory;
    private IUnitOfWorkBulletin _unitOfWork;
    private IBulletinDtoValidatorFacade _validator;
    private IMapper _autoMapper;

    /// <inheritdoc/>
    public BulletinService // Не рабочий
        (
        IBulletinReposotory bulletinReposotory,
        IUnitOfWorkBulletin unitOfWork,
        IBulletinDtoValidatorFacade validator,
        IMapper automapper
        )
    {
        _bulletinReposotory = bulletinReposotory;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _autoMapper = automapper;
    }



    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(BulletinCreateDtoController createDto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            BelletinCreateDto belletinCreateDto = new(createDto.BulletinMain);
            belletinCreateDto.AddCharacteristicComparisons(createDto.CharacteristicComparisons);
            belletinCreateDto.AddImages(createDto.Images);
            belletinCreateDto.AddViewsCount();

            await _validator.ValidateThrowValidationExeptionAsync(belletinCreateDto);

            Guid bulletinId = await _bulletinReposotory.CreateAsync(belletinCreateDto, cancellationToken);

            await _unitOfWork.CommitTransactionAsync();

            return bulletinId; 
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    //private async Task<Guid> CreateBulletinMainAsync(BulletinMainCreateDto mainDto, CancellationToken cancellationToken)
    //{
    //    var BulletinMain = await _mainService.CreateAsync(mainDto, cancellationToken);
    //    Guid bulletinId = BulletinMain.Id;
    //    return bulletinId;
    //}

    //private async Task CreateCharacteristicsAsync(List<BulletinCharacteristicComparisonCreateDtoWhileBulletinCreating> characteristics, Guid bulletinId, CancellationToken cancellationToken)
    //{
    //    foreach (var characteristic in characteristics)
    //    {
    //        var createDto = _autoMapper.Map<BulletinCharacteristicComparisonCreateDto>(characteristic);
    //        createDto.BulletinId = bulletinId;
    //        await _characteristicComparisonService.CreateAsync(createDto, cancellationToken);
    //    }
    //}

    //private async Task CreateViewsCountAsync(Guid bulletinId, CancellationToken cancellationToken)
    //{
    //    //await _viewsCountService.CreateAsync(
    //    //    new BulletinViewsCountCreateDto {
    //    //    BulletinId = bulletinId, 
    //    //    ViewsCount = 0 
    //    //}, cancellationToken);

    //    await _unitOfWork._viewsCountRepository.CreateAsync(
    //        new BulletinViewsCountCreateDto
    //        {
    //            BulletinId = bulletinId,
    //            ViewsCount = 0
    //        }, cancellationToken);
    //}

    //private async Task CreateImagesAsync(List<BulletinImageCreateDtoWhileBulletinCreating> images, Guid bulletinId, CancellationToken cancellationToken)
    //{
    //    //foreach (var image in images)
    //    //{
    //    //    var createDto = _autoMapper.Map<BulletinImageCreateDto>(image);
    //    //    createDto.BulletinId = bulletinId;
    //    //    await _imageService.CreateAsync(createDto, cancellationToken);
    //    //}

    //    foreach (var image in images)
    //    {
    //        var createDto = _autoMapper.Map<BulletinImageCreateDto>(image);
    //        createDto.BulletinId = bulletinId;
    //        await _unitOfWork._imageRepository.CreateAsync(createDto,cancellationToken);
    //    }
    //}
}
