using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinMainValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingDtoValidatorFacade.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BelletinMain;
using BulletinBoard.Contracts.Bulletin.BulletinRating.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinRating.UpdateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinRatingValidator;

/// <inheritdoc/>
public class BulletinRatingDtoValidatorFacade : BaseValidatorFacade
    <
    BulletinRatingCreateDto,
    BulletinRatingUpdateDtoForValidating,
    IBulletinRatingCreateDtoValidator,
    IBulletinRatingUpdateDtoValidator,
    IBulletinRatingDeleteValidator
    >, IBulletinRatingDtoValidatorFacade
{
    /// <inheritdoc/>
    public BulletinRatingDtoValidatorFacade
        (
        IBulletinRatingCreateDtoValidator bulletinCategoryCreateDtoValidator, 
        IBulletinRatingUpdateDtoValidator bulletinCategoryUpdateDtoValidator, 
        IBulletinRatingDeleteValidator deleteValidator
        ) : base(bulletinCategoryCreateDtoValidator, bulletinCategoryUpdateDtoValidator, deleteValidator)
    {
    }
}
