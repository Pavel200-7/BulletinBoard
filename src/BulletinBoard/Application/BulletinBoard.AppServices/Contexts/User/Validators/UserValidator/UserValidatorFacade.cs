using BulletinBoard.AppServices.Contexts.User.Validators.UserValidator.IValidators;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Contracts.Images.Image.CreateDto;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using FluentValidation.Results;


namespace BulletinBoard.AppServices.Contexts.User.Validators.UserValidator;

/// <inheritdoc/>
public class UserValidatorFacade : IUserValidatorFacade
{
    private readonly IValidators.IUserCreateDtoValidator _createDtoValidator;

    /// <inheritdoc/>
    public UserValidatorFacade
        (
        IValidators.IUserCreateDtoValidator createDtoValidator
        )
    {
        _createDtoValidator = createDtoValidator;

    }

    /// <inheritdoc/>
    public async Task ValidateThrowValidationExeptionAsync(ApplicationUserCreateDto entityDto)
    {
        var validationResult = await _createDtoValidator.ValidateAsync(entityDto);
        CheckValidationResult(validationResult);
    }

    /// <summary>
    /// Проверяет результат валидации и вызывает исключени при наличии ошибок.
    /// </summary>
    /// <param name="validationResult">Результат валидации.</param>
    /// <exception cref="ValidationExeption">Ничего или исключение.</exception>
    protected void CheckValidationResult(ValidationResult validationResult)
    {
        if (!validationResult.IsValid)
        {
            throw new ValidationExeption(validationResult.ToDictionary());
        }
    }
}
