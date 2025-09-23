using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Errors.Exeptions;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;

/// <summary>
/// Базовый класс фасада валидатора.
/// Он требует вставки всех указанных валидаторов.
/// Если для валидация определенноего дто не требуется, то
/// валмдатор создать все равно нужно, но делать проверки в 
/// нем не обязательо.
/// </summary>
/// <typeparam name="TCreateDtoValidator"></typeparam>
/// <typeparam name="TUpdateDtoValidator"></typeparam>
/// <typeparam name="TDeleteValidator"></typeparam>
/// <typeparam name="TCreateDto"></typeparam>
/// <typeparam name="TUpdateDto"></typeparam>
public abstract class BaseValidatorFacade
    <
    TCreateDto,
    TUpdateDto,
    TCreateDtoValidator, 
    TUpdateDtoValidator, 
    TDeleteValidator
    >
    where TCreateDto : class
    where TUpdateDto : class
    where TCreateDtoValidator : class, IDtoValidator<TCreateDto>
    where TUpdateDtoValidator : class, IDtoValidator<TUpdateDto>
    where TDeleteValidator : IDeleteValidator
{
    private readonly TCreateDtoValidator _createDtoValidator;
    private readonly TUpdateDtoValidator _updateDtoValidator;
    private readonly TDeleteValidator _deleteValidator;


    /// <inheritdoc/>
    public BaseValidatorFacade
        (
        TCreateDtoValidator bulletinCategoryCreateDtoValidator,
        TUpdateDtoValidator bulletinCategoryUpdateDtoValidator,
        TDeleteValidator deleteValidator
        )
    {
        _createDtoValidator = bulletinCategoryCreateDtoValidator;
        _updateDtoValidator = bulletinCategoryUpdateDtoValidator;
        _deleteValidator = deleteValidator;
    }

    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateAsync(TCreateDto entityDto)
    {
        return await _createDtoValidator.ValidateAsync(entityDto);
    }

    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateAsync(TUpdateDto entityDto)
    {
        return await _updateDtoValidator.ValidateAsync(entityDto);
    }

    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateBeforeDeletingAsync(Guid entityId)
    {
        return await _deleteValidator.ValidateAsync(entityId);
    }

    /// <inheritdoc/>
    public async Task ValidateThrowValidationExeptionAsync(TCreateDto entityDto)
    {
        var validationResult = await _createDtoValidator.ValidateAsync(entityDto);
        CheckValidationResult(validationResult);
    }

    /// <summary>
    /// Валидировать ДТО обновления сущности и выбрасывает исключение при ошибке валидации.
    /// </summary>
    public async Task ValidateThrowValidationExeptionAsync(TUpdateDto entityDto)
    {
        var validationResult = await _updateDtoValidator.ValidateAsync(entityDto);
        CheckValidationResult(validationResult);
    }

    /// <summary>
    /// Валидировать сущность по id до ее удаления и выбрасывает исключение при ошибке валидации.
    /// </summary>
    /// <param name="entityId">id сущности.</param>
    /// <returns></returns>
    public async Task ValidateBeforeDeletingThrowValidationExeptionAsync(Guid entityId)
    {
        var validationResult = await _deleteValidator.ValidateAsync(entityId);
        CheckValidationResult(validationResult);
    }

    private void CheckValidationResult(ValidationResult validationResult) 
    {
        if (!validationResult.IsValid)
        {
            throw new ValidationExeption(validationResult.ToDictionary());
        }
    }
}
