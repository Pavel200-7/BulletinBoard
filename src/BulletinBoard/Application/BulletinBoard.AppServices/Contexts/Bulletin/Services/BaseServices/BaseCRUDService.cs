using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.AppServices.Repository;
using BulletinBoard.AppServices.Specification;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Domain.Entities.Bulletin;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Services.BaseServices;

/// <summary>
/// Базовый сервис, включающий в себя только 4 CRUD операции.
/// </summary>
/// <typeparam name="TEntityDto">Базовый ДТО.</typeparam>
/// <typeparam name="TEntityCreateDto">ДТО создания сущности.</typeparam>
/// <typeparam name="TEntityUpdateDto">ДТО обновления сущности.</typeparam>
/// <typeparam name="TValidationEntityUpdateDto">ДТО валидации обновления сущности.</typeparam>
/// <typeparam name="TRepository">Репозиторий для работы с сущностью.</typeparam>
/// <typeparam name="TDtoValidatorFacade">Валидатор.</typeparam>
public abstract class BaseCRUDService
    <
    TEntityDto,
    TEntityCreateDto,
    TEntityUpdateDto,
    TValidationEntityUpdateDto,
    TRepository,
    TDtoValidatorFacade
    > : IBaseCRUDService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    where TEntityDto : class
    where TEntityCreateDto : class
    where TEntityUpdateDto : class
    where TValidationEntityUpdateDto : class
    where TRepository : ICRUDRepository<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    where TDtoValidatorFacade : IValidatorFacade<TEntityCreateDto, TValidationEntityUpdateDto>
{
    /// <summary>
    /// Репозитой.
    /// </summary>
    protected readonly TRepository _repository;
    /// <summary>
    /// Валидатор.
    /// </summary>
    protected readonly TDtoValidatorFacade _validator;
    /// <summary>
    ///  Автомаппер.
    /// </summary>
    protected readonly IMapper _autoMapper;

    /// <summary>
    /// Название сущности, которое используется для сообщения об ошибках.
    /// </summary>
    protected abstract string EntityName {  get; }

    /// <inheritdoc/>
    public BaseCRUDService
        (
        TRepository repository,
        TDtoValidatorFacade validator,
        IMapper autoMapper
        )
    {
        _repository = repository;
        _validator = validator;
        _autoMapper = autoMapper;
    }

    /// <inheritdoc/>
    public async Task<TEntityDto> GetByIdAsync(Guid id)
    {
        TEntityDto? outputDto = await _repository.GetByIdAsync(id);
        if (outputDto is null) { throw new NotFoundException(GetNotFoundIdMessage(id)); }
        return outputDto;
    }

    /// <inheritdoc/>
    public async Task<TEntityDto> CreateAsync(TEntityCreateDto createDto, CancellationToken cancellationToken)
    {
        await _validator.ValidateThrowValidationExeptionAsync(createDto);
        TEntityDto outputDto = await _repository.CreateAsync(createDto, cancellationToken);
        return outputDto;
    }

    /// <inheritdoc/>
    public async Task<TEntityDto> UpdateAsync(Guid id, TEntityUpdateDto updateDto, CancellationToken cancellationToken)
    {
        TValidationEntityUpdateDto updateValidationDto =  await GetUpdateValidationDto(id, updateDto);
        await _validator.ValidateThrowValidationExeptionAsync(updateValidationDto);
        TEntityDto? outputDto = await _repository.UpdateAsync(id, updateDto, cancellationToken);

        if (outputDto is null) { throw new NotFoundException(GetNotFoundIdMessage(id)); }

        return outputDto;
    }

    /// <summary>
    /// Получить дто, используемое для валидации обновления данных.
    /// Иногда для валидации нужны даже те данные, которые по логике обновлять нельзя.
    /// Брать их у пользователя некрасиво, поэтому их лучше взять из БД. 
    /// Если ДТО обновления и дто валидации совпадают, нужно просто перемапить один в другой.
    /// Если нет, нужно пойти в БД, получить сущность по id, перемапипь ее в дто валидации и 
    /// присвоить ей данные поступающей ДТО обновления.
    /// </summary>
    /// <param name="id">id обновляемой сущности.</param>
    /// <param name="updateDto">dto обновления.</param>
    /// <returns>Дто валидации обновления.</returns>
    protected abstract Task<TValidationEntityUpdateDto> GetUpdateValidationDto(Guid id, TEntityUpdateDto updateDto);

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _validator.ValidateBeforeDeletingThrowValidationExeptionAsync(id);
        bool isOnDeleting = await _repository.DeleteAsync(id, cancellationToken);

        if (!isOnDeleting) { throw new NotFoundException(GetNotFoundIdMessage(id)); }

        return isOnDeleting;
    }

    /// <summary>
    ///  Выдать сообщение о том, что сущность с данным id не найдена.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    protected string GetNotFoundIdMessage(Guid id)
    {
        return $"The {EntityName} with id {id} is not found.";
    }
}
