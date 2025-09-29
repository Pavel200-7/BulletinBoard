using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.UpdateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.UpdateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator;

/// <inheritdoc/>
public class BulletinDtoValidatorFacade : BaseValidatorFacade
    <
    BelletinCreateDto,
    BelletinUpdateDtoForValidating,
    IBulletinCreateDtoValidator,
    IBulletinUpdateDtoValidator,
    IBulletinDeleteValidator
    >, IBulletinDtoValidatorFacade
{
    /// <inheritdoc/>
    public BulletinDtoValidatorFacade
        (
        IBulletinCreateDtoValidator bulletinCategoryCreateDtoValidator, 
        IBulletinUpdateDtoValidator bulletinCategoryUpdateDtoValidator, 
        IBulletinDeleteValidator deleteValidator
        ) : base(bulletinCategoryCreateDtoValidator, bulletinCategoryUpdateDtoValidator, deleteValidator)
    {
    }
}
