using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator;

/// <inheritdoc/>
public class BulletinUserDtoValidatorFacade : BaseValidatorFacade
    <
    BulletinUserCreateDto,
    BulletinUserUpdateDto,
    BulletinUserCreateDtoValidator,
    BulletinUserUpdateDtoValidator,
    IBulletinUserDeleteValidator
    >, IBulletinUserDtoValidatorFacade
{
    /// <inheritdoc/>
    public BulletinUserDtoValidatorFacade
        (
        BulletinUserCreateDtoValidator bulletinCategoryCreateDtoValidator, 
        BulletinUserUpdateDtoValidator bulletinCategoryUpdateDtoValidator, 
        IBulletinUserDeleteValidator deleteValidator
        ) : base(bulletinCategoryCreateDtoValidator, bulletinCategoryUpdateDtoValidator, deleteValidator)
    {
    }
}
