using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicComparisonValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison;
using BulletinBoard.Contracts.Bulletin.BulletinCharacteristicComparison.ForValidating;
using BulletinBoard.Contracts.Bulletin.BulletinImage;
using BulletinBoard.Contracts.Bulletin.BulletinImage.ForValidating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinImageValidator;

/// <inheritdoc/>
public class BulletinImageDtoValidatorFacade : BaseValidatorFacade
    <
    BulletinImageCreateDto,
    BulletinImageUpdateDtoForValidating,
    IBulletinImageCreateDtoValidator,
    IBulletinImageUpdateDtoValidator,
    IBulletinImageDeleteValidator
    >, IBulletinImageDtoValidatorFacade
{
    /// <inheritdoc/>
    public BulletinImageDtoValidatorFacade
        (
        IBulletinImageCreateDtoValidator bulletinCategoryCreateDtoValidator,
        IBulletinImageUpdateDtoValidator bulletinCategoryUpdateDtoValidator,
        IBulletinImageDeleteValidator deleteValidator
        ) : base(bulletinCategoryCreateDtoValidator, bulletinCategoryUpdateDtoValidator, deleteValidator)
    {
    }
}
