using BulletinBoard.Contracts.Images.Image.CreateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Images.Validators.ImageValidator.IValidators;

/// <summary>
///  Валидатор изображения.
/// </summary>
public interface IImageValidatorFacade
{
    /// <summary>
    /// Валидировать изображение
    /// </summary>
    /// <param name="entityDto">Дто создания изображения.</param>
    /// <returns>Ошибка валидации при несоответствии.</returns>
    public Task ValidateThrowValidationExeptionAsync(ImageCreateDto entityDto);
}
