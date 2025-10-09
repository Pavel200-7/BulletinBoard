using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.AppServices.Contexts.Images.Validators.ImageValidator.IValidators;
using BulletinBoard.Contracts.Errors.Exeptions;
using BulletinBoard.Contracts.Images.Image.CreateDto;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Images.Validators.ImageValidator;

/// <inheritdoc/>
public class ImageValidatorFacade : IImageValidatorFacade
{
    private readonly IImageCreateDtoValidator _createDtoValidator;

    /// <inheritdoc/>
    public ImageValidatorFacade
        (
        IImageCreateDtoValidator createDtoValidator
        )
    {
        _createDtoValidator = createDtoValidator;

    }

    /// <inheritdoc/>
    public async Task ValidateThrowValidationExeptionAsync(ImageCreateDto entityDto)
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
