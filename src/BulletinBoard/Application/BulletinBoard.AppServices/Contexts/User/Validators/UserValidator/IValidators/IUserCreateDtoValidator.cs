using BulletinBoard.Contracts.Images.Image.CreateDto;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.User.Validators.UserValidator.IValidators;

/// <summary>
/// Валидатор, проверяющий пользователя при создании по следующим показателям:
/// UserName:
///     1. Заполнен.
///     2. Имеет длину от 3 до 50 символов.
///     3. Содержит только символы: [a-zA-Zа-яА-ЯёЁ0-9_.\-]
///     4. Не содержит только цифры.
///     5. Не содержит только спецсимволы.
/// PhoneNumber:
///     1. Соответствует формату.
/// Latitude:
///     1. от -90 до 90
/// Longitude:
///     1. 1. от -180 до 180
/// FormattedAddress:
///     1. Имеет длину от до 500 символов.
///     2. Не содержит неуместных символов.
/// </summary>
public interface IUserCreateDtoValidator
{
    /// <summary>
    /// Стандартизированный метод валидации.
    /// </summary>
    /// <param name="entityDto">ДТО сущности.</param>
    /// <param name="cancellation">Токен отмены, ставится автоматически.</param>
    /// <returns>Результат валидации.</returns>
    public Task<ValidationResult> ValidateAsync(ApplicationUserCreateDto entityDto, CancellationToken cancellation = default);
}
