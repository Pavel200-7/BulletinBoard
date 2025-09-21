using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinUser;
using FluentValidation;


namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinUserValidator;

/// <inheritdoc/>
public class BulletinUserUpdateDtoValidator : AbstractValidator<BulletinUserUpdateDto>, IBulletinUserUpdateDtoValidator
{
    /// <inheritdoc/>
    public BulletinUserUpdateDtoValidator()
    {
        // Валидации нет. (не предполагается текущими требованиями)
    }
}
