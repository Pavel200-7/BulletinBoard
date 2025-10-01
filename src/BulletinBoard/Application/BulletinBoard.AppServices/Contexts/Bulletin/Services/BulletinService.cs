using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.ReadDto;
using BulletinBoard.Contracts.Errors.Exeptions;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinService : IBulletinService
{
    private IBulletinReposotory _bulletinReposotory;
    private IUnitOfWorkBulletin _unitOfWork;
    private IBulletinDtoValidatorFacade _validator;

    /// <inheritdoc/>
    public BulletinService // Не рабочий
        (
        IBulletinReposotory bulletinReposotory,
        IUnitOfWorkBulletin unitOfWork,
        IBulletinDtoValidatorFacade validator
        )
    {
        _bulletinReposotory = bulletinReposotory;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    /// <inheritdoc/>
    public async Task<BulletinDto> GetByIdAsync(Guid id)
    {
        BulletinDto? outputDto = await _bulletinReposotory.GetByIdAsync(id);
        if (outputDto is null) 
        {
            string message = $"The bulletin with id {id} is not found.";
            throw new NotFoundException(message); 
        }

        return outputDto;
    }

    /// <inheritdoc/>
    public async Task<BulletinReadSingleDto> GetByIdReadSingleAsync(Guid id)
    {
        BulletinReadSingleDto? outputDto = await _bulletinReposotory.GetByIdReadSingleAsync(id);
        if (outputDto is null)
        {
            string message = $"The bulletin with id {id} is not found.";
            throw new NotFoundException(message);
        }

        return outputDto;
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


}
