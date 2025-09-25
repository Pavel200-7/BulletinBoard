using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinRating.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.UpdateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных ДТО сущности BulletinRating
/// </summary>
public interface IBulletinRatingDtoValidatorFacade : IValidatorFacade<BulletinRatingCreateDto, BulletinRatingUpdateDtoForValidating>
{
}
