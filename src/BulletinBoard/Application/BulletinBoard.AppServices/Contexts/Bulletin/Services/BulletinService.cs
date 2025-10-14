using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.IValidators;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.FilterDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.ReadDto;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Domain.Entities.Bulletin;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Services;

/// <inheritdoc/>
public class BulletinService : IBulletinService
{
    private IBulletinReposotory _bulletinReposotory;
    private IUnitOfWorkBulletin _unitOfWork;
    private IBulletinDtoValidatorFacade _validator;
    private IBulletinMainSpecificationBuilder _specificationBuilder;
    private IBulletinMainCursorPaginationSpecificationBuilder _paginationSpecificationBuilder;

    /// <inheritdoc/>
    public BulletinService // Не рабочий
        (
        IBulletinReposotory bulletinReposotory,
        IUnitOfWorkBulletin unitOfWork,
        IBulletinDtoValidatorFacade validator,
        IBulletinMainSpecificationBuilder specificationBuilder,
        IBulletinMainCursorPaginationSpecificationBuilder paginationSpecificationBuilder

        )
    {
        _bulletinReposotory = bulletinReposotory;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _specificationBuilder = specificationBuilder;
        _paginationSpecificationBuilder = paginationSpecificationBuilder;
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

    public async Task<BulletinReadPagenatedDto> GetBulletinsAsync(BulletinPaginationRequestDto requestDto)
    {
        await _validator.ValidateThrowValidationExeptionAsync(requestDto);

        var specification = _specificationBuilder
            .WhereCategoryId(requestDto.CategoryId)
            .WherePriceRange(requestDto.MinPrice, requestDto.MaxPrice)
            .WhereTitleContains(requestDto.SearchText)
            .Build();

        _paginationSpecificationBuilder.SetSpecification(specification);

        Guid? lastId = requestDto.LastId;
        bool ascending = requestDto.SortOrder.ToLower() == "asc" ? true : false;
        switch (requestDto.SortBy)
        {
            case "title":
                _paginationSpecificationBuilder.PaginateByTitle(lastId, requestDto.LastTitle, ascending);
                break;
            case "date": 
                _paginationSpecificationBuilder.PaginateByDate(lastId, requestDto.LastDate, ascending);
                break;
            case "price":
                _paginationSpecificationBuilder.PaginateByPrice(lastId, requestDto.LastPrice, ascending);
                break;
        }

        _paginationSpecificationBuilder.Take(requestDto.Limit + 1);
        specification = _paginationSpecificationBuilder.Build();

        var bulletins = await _bulletinReposotory.GetBulletinsAsync(specification);
        var bulletinsList = bulletins.ToList();

        var response = new BulletinReadPagenatedDto(bulletinsList, requestDto);

        return response;
    }


    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(BulletinCreateDtoController createDto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            BulletinCreateDto belletinCreateDto = new(createDto.BulletinMain);
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
