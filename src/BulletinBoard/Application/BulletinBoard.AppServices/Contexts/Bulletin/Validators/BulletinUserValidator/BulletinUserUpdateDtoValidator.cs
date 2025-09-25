using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using BulletinBoard.Contracts.Bulletin.BulletinUser.UpdateDto;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator;

/// <inheritdoc/>
public class BulletinUserUpdateDtoValidator : AbstractValidator<BulletinUserUpdateDtoForValidating>, IBulletinUserUpdateDtoValidator
{
    /// <inheritdoc/>
    public BulletinUserUpdateDtoValidator()
    {
        // Валидации нет. (не предполагается текущими требованиями)
    }
}
