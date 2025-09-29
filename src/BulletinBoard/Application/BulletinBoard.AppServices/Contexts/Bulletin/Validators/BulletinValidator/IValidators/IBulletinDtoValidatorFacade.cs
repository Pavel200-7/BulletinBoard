using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator.IBaseValidator;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.UpdateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.IValidators;

/// <summary>
/// Класс, предоставляющий единый интерфейс для валидации разных дто работы с объявлением (как совокупности связанных сущностей)
/// </summary>
public interface IBulletinDtoValidatorFacade : IValidatorFacade<BelletinCreateDto, BelletinUpdateDtoForValidating>
{
}
