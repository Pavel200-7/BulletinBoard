using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinRating.CreateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО создания рейтинга по правилам:
/// UserId:
///     1. Пользователь с таким id существует.
///     2. Пользователь с таким id не заблокирован.
/// BulletinId:
///     1. Объявление с таким id существует.
///     2. Объявление с таким id не заблокировано
/// Rating:
///     1. Не null.
///     2. Целое число в диапазоне от 1 до 10.
/// </summary>
public interface IBulletinRatingCreateDtoValidator : IDtoValidator<BulletinRatingCreateDto>
{
}
