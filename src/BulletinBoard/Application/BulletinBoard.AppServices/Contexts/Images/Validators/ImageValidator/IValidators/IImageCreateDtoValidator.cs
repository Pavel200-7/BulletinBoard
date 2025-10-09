using BulletinBoard.Contracts.Images.Image.CreateDto;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Images.Validators.ImageValidator.IValidators;

/// <summary>
/// Валидатор, проверяющий изображение при создании по следующим показателям:
/// 
/// </summary>
public interface IImageCreateDtoValidator
{
    /// <summary>
    /// Стандартизированный метод валидации.
    /// </summary>
    /// <param name="entityDto">ДТО сущности.</param>
    /// <param name="cancellation">Токен отмены, ставится автоматически.</param>
    /// <returns>Результат валидации.</returns>
    public Task<ValidationResult> ValidateAsync(ImageCreateDto entityDto, CancellationToken cancellation = default);
}
