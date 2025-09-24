using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinRating;
using BulletinBoard.Contracts.Bulletin.BulletinRating.ForValidating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator.IValidators;

/// <summary>
/// Валидатор проводящий проверку ДТО обновления рейтинга по правилам:
/// Rating:
///     1. Не null.
///     2. Целое число в диапазоне от 1 до 10.
/// </summary>
public interface IBulletinRatingUpdateDtoValidator : IDtoValidator<BulletinRatingUpdateDtoForValidating>
{
}
