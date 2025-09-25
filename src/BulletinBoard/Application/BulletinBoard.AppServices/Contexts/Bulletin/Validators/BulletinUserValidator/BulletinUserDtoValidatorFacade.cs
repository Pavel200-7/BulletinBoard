using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BaseValidator;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCharacteristicValueValidator.IValidators;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinUser.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinUser.UpdateDto;
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
    BulletinUserUpdateDtoForValidating,
    IBulletinUserCreateDtoValidator,
    IBulletinUserUpdateDtoValidator,
    IBulletinUserDeleteValidator
    >, IBulletinUserDtoValidatorFacade
{
    /// <inheritdoc/>
    public BulletinUserDtoValidatorFacade
        (
        IBulletinUserCreateDtoValidator bulletinCategoryCreateDtoValidator,
        IBulletinUserUpdateDtoValidator bulletinCategoryUpdateDtoValidator, 
        IBulletinUserDeleteValidator deleteValidator
        ) : base(bulletinCategoryCreateDtoValidator, bulletinCategoryUpdateDtoValidator, deleteValidator)
    {
    }
}
