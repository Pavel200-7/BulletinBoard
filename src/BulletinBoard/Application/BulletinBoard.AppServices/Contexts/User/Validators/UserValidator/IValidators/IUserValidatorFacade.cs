using BulletinBoard.Contracts.Images.Image.CreateDto;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.User.Validators.UserValidator.IValidators;

/// <summary>
///  Валидатор пользователя.
/// </summary>
public interface IUserValidatorFacade
{
    /// <summary>
    /// Валидировать пользователя.
    /// </summary>
    /// <param name="entityDto">Дто создания пользователя.</param>
    /// <returns>Ошибка валидации при несоответствии.</returns>
    public Task ValidateThrowValidationExeptionAsync(ApplicationUserCreateDto entityDto);
}
